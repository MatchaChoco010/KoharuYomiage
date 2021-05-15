using System;

namespace KoharuYomiageApp.Entities
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
