using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MisskeyApi.Exceptions;
using MisskeyApi.Payloads;
using MisskeyApi.Payloads.Entities;
using MisskeyApi.Stream;

namespace MisskeyApi
{
    public static class Api
    {
        static readonly HttpClientHandler s_httpClientHandler = new();

        static async Task<bool> CheckInstance(string hostName, CancellationToken cancellationToken = new())
        {
            if (Uri.CheckHostName(hostName) == UriHostNameType.Unknown)
            {
                throw new ArgumentException("hostName must be host string");
            }
            
            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = hostName,
                Path = "api/meta",
            };
            
            using var client = new HttpClient(s_httpClientHandler, false);
            var content = new StringContent("{\"detail\": false}", Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uriBuilder.Uri, content, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }

        public static async Task<(SessionId, Uri)> GetAuthorizeUri(string hostName,
            CancellationToken cancellationToken = new())
        {
            if (Uri.CheckHostName(hostName) == UriHostNameType.Unknown)
            {
                throw new ArgumentException("hostName must be host string");
            }

            if (!await CheckInstance(hostName, cancellationToken))
            {
                throw new ArgumentException("hostName is not misskey instance");
            }

            var sessionId = new SessionId(Guid.NewGuid().ToString());

            var parameters = new List<KeyValuePair<string, string>>
            {
                new("name", "小春六花さんにTLを読み上げていただくアプリ"),
                new("icon", "https://raw.githubusercontent.com/MatchaChoco010/KoharuYomiage/main/appicon.gif"),
                new("permission", "read:account,read:notifications"),
            };
            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = hostName,
                Path = $"miauth/{sessionId.Value}",
                Query = await new FormUrlEncodedContent(parameters).ReadAsStringAsync(),
            };

            return (sessionId, uriBuilder.Uri);
        }

        public static async Task<(AccessToken, User)> GetAccessToken(string hostName, SessionId sessionId,
            CancellationToken cancellationToken = new())
        {
            if (Uri.CheckHostName(hostName) == UriHostNameType.Unknown)
            {
                throw new ArgumentException("hostName must be host string");
            }

            var uriBuilder = new UriBuilder
            {
                Scheme = "https", Host = hostName, Path = $"api/miauth/{sessionId.Value}/check"
            };

            using var client = new HttpClient(s_httpClientHandler, false);
            var response = await client.PostAsync(uriBuilder.Uri, null, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpResponseException(response.StatusCode, $"Unable to connect {uriBuilder.Uri}");
            }

            var json = (await JsonSerializer.DeserializeAsync<GetAccessTokenResponseJson>(
                await response.Content.ReadAsStreamAsync(), cancellationToken: cancellationToken))!;

            return (new AccessToken(json.token), json.user);
        }

        public static IObservable<UserStreamPayload> GetUserStreamingObservable(string hostName,
            AccessToken accessToken)
        {
            return new UserStreamObservable(hostName, accessToken);
        }

        record GetAccessTokenResponseJson(string token, User user);
    }
}
