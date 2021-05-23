using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;

namespace KoharuYomiageApp.Domain.Client.Mastodon
{
    public class MastodonClient
    {
        public MastodonClient(Instance instance, MastodonClientId clientId, MastodonClientSecret clientSecret)
        {
            Instance = instance;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        public Instance Instance { get; init; }
        public MastodonClientId ClientId { get; init; }
        public MastodonClientSecret ClientSecret { get; init; }

        public async Task<Uri> GetAuthorizeUri()
        {
            var parameters = new List<KeyValuePair<string, string>>
            {
                new("force_login", "true"),
                new("response_type", "code"),
                new("client_id", ClientId.Value),
                new("redirect_uri", "urn:ietf:wg:oauth:2.0:oob"),
                new("scope", "read:accounts read:statuses read:notifications")
            };
            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = Instance.Value,
                Path = "oauth/authorize",
                Query = await new FormUrlEncodedContent(parameters).ReadAsStringAsync()
            };
            return uriBuilder.Uri;
        }
    }
}
