using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
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

        public static async Task<Secret> RegisterApp(string hostName, CancellationToken cancellationToken = new())
        {
            if (Uri.CheckHostName(hostName) == UriHostNameType.Unknown)
            {
                throw new ArgumentException("hostName must be host string");
            }

            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = hostName,
                Path = "api/app/create"
            };
            var requestJson = JsonSerializer.Serialize(new
            {
                name = "小春六花さんにTLを読み上げていただくアプリ",
                description = "小春六花さんにTLを読み上げていただくアプリケーション",
                permission = new List<string>{ "read:account", "read:notifications" },
            });

            using var client = new HttpClient(s_httpClientHandler, false);
            var response = await client.PostAsync(uriBuilder.Uri, new StringContent(requestJson), cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpResponseException(response.StatusCode, $"Unable to connect {uriBuilder.Uri}");
            }

            var json = (await JsonSerializer.DeserializeAsync<RegisterAppResponceJson>(
                await response.Content.ReadAsStreamAsync(), cancellationToken: cancellationToken))!;

            return new Secret(json.secret);
        }

        public static async Task<(SessionToken, Uri)> GetAuthorizeUri(string hostName, Secret secret,
            CancellationToken cancellationToken = new())
        {
            if (Uri.CheckHostName(hostName) == UriHostNameType.Unknown)
            {
                throw new ArgumentException("hostName must be host string");
            }

            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = hostName,
                Path = "api/auth/session/generate"
            };
            var requestJson = JsonSerializer.Serialize(new
            {
                appSecret = secret.Value,
            });

            using var client = new HttpClient(s_httpClientHandler, false);
            var response = await client.PostAsync(uriBuilder.Uri, new StringContent(requestJson), cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpResponseException(response.StatusCode, $"Unable to connect {uriBuilder.Uri}");
            }

            var json = (await JsonSerializer.DeserializeAsync<GetAuthorizeUriResponseJson>(
                await response.Content.ReadAsStreamAsync(), cancellationToken: cancellationToken))!;

            return (new SessionToken(json.token), new Uri(json.url));
        }

        public static async Task<(AccessToken, User)> GetAccessToken(string hostName, Secret secret, SessionToken token,
            CancellationToken cancellationToken = new())
        {
            if (Uri.CheckHostName(hostName) == UriHostNameType.Unknown)
            {
                throw new ArgumentException("hostName must be host string");
            }

            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = hostName,
                Path = "api/auth/session/userkey"
            };
            var requestJson = JsonSerializer.Serialize(new
            {
                appSecret = secret.Value,
                token = token.Value,
            });

            using var client = new HttpClient(s_httpClientHandler, false);
            var response = await client.PostAsync(uriBuilder.Uri, new StringContent(requestJson), cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpResponseException(response.StatusCode, $"Unable to connect {uriBuilder.Uri}");
            }

            var json = (await JsonSerializer.DeserializeAsync<GetAccessTokenResponseJson>(
                await response.Content.ReadAsStreamAsync(), cancellationToken: cancellationToken))!;

            using var sha256 = SHA256.Create();
            var data = sha256.ComputeHash(Encoding.UTF8.GetBytes(json.accessToken + secret.Value));
            var stringBuilder = new StringBuilder();
            foreach (var b in data)
            {
                stringBuilder.Append(b.ToString("x2"));
            }
            var accessToken = stringBuilder.ToString();

            return (new AccessToken(accessToken), json.user);
        }

        public static IObservable<UserStreamPayload> GetUserStreamingObservable(string hostName,
            AccessToken accessToken)
        {
            return new UserStreamObservable(hostName, accessToken);
        }

        record RegisterAppResponceJson(string id, string name, string description, string? callbackUrl,
            List<string> permission, string secret);

        record GetAuthorizeUriResponseJson(string token, string url);

        record GetAccessTokenResponseJson(string accessToken, User user);
    }
}
