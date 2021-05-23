using System;

namespace KoharuYomiageApp.Domain.Account.Mastodon
{
    public record MastodonAccessToken
    {
        public MastodonAccessToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("AccessToken must not be empty");
            }

            Token = token;
        }

        public string Token { get; }
    }
}
