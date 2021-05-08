using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MastodonApi.Exceptions;

namespace MastodonApi
{
    public static class MastodonApi
    {
        static readonly HttpClientHandler s_httpClientHandler = new();

        public static async ValueTask<(ClientId, ClientSecret)> RegisterApp(string hostName)
        {
            if (Uri.CheckHostName(hostName) == UriHostNameType.Unknown)
            {
                throw new ArgumentException("hostName must be host string");
            }

            var parameters = new List<KeyValuePair<string, string>>
            {
                new("client_name", "小春六花さんにTLを読み上げていただくアプリ"),
                new("website", "https://github.com/MatchaChoco010/KoharuYomiage"),
                new("redirect_uris", "urn:ietf:wg:oauth:2.0:oob"),
                new("scopes", "read:statuses read:notifications")
            };
            var content = new FormUrlEncodedContent(parameters);
            var uriBuilder = new UriBuilder {Scheme = "https", Host = hostName, Path = "api/v1/apps"};

            using var client = new HttpClient(s_httpClientHandler, false);
            var response = await client.PostAsync(uriBuilder.Uri, content);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpResponseException(response.StatusCode, $"Unable to connect {uriBuilder.Uri}");
            }

            var json = (await JsonSerializer.DeserializeAsync<RegisterAppJson>(
                await response.Content.ReadAsStreamAsync()))!;

            return (new ClientId(json.client_id), new ClientSecret(json.client_secret));
        }

        public static async ValueTask<Uri> GetAuthorizeUri(string hostName, ClientId clientId)
        {
            if (Uri.CheckHostName(hostName) == UriHostNameType.Unknown)
            {
                throw new ArgumentException("hostName must be host string");
            }

            var parameters = new List<KeyValuePair<string, string>>()
            {
                new("force_login", "true"),
                new("response_type", "code"),
                new("client_id", clientId.Id),
                new("redirect_uri", "urn:ietf:wg:oauth:2.0:oob"),
                new("scope", "read:statuses read:notifications"),
            };
            var uriBuilder = new UriBuilder()
            {
                Scheme = "https",
                Host = hostName,
                Path = "oauth/authorize",
                Query = await new FormUrlEncodedContent(parameters).ReadAsStringAsync()
            };
            return uriBuilder.Uri;
        }

        public static async ValueTask<AccessToken> AuthorizeWithCode(string hostName, ClientId clientId, ClientSecret clientSecret, string code)
        {
            if (Uri.CheckHostName(hostName) == UriHostNameType.Unknown)
            {
                throw new ArgumentException("hostName must be host string");
            }
            
            var parameters = new List<KeyValuePair<string, string>>()
            {
                new("client_id", clientId.Id),
                new("client_secret", clientSecret.Secret),
                new("redirect_uri", "urn:ietf:wg:oauth:2.0:oob"),
                new("scope", "code"),
                new("grant_type", "authorization_code")
            };
            var content = new FormUrlEncodedContent(parameters);
            var uriBuilder = new UriBuilder() {Scheme = "https", Host = hostName, Path = "oauth/token"};

            using var client = new HttpClient(s_httpClientHandler, false);
            var response = await client.PostAsync(uriBuilder.Uri, content);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpResponseException(response.StatusCode, $"Unable to connect {uriBuilder.Uri}");
            }

            var json = (await JsonSerializer.DeserializeAsync<AccessTokenJson>(
                await response.Content.ReadAsStreamAsync()))!;

            return new AccessToken(json.access_token);
        }

        record RegisterAppJson(string id, string name, string? website, string redirect_uri, string client_id,
            string client_secret, string vapid_key);

        record AccessTokenJson(string access_token, string token_type, string scope, DateTime created_at);
    }
}
