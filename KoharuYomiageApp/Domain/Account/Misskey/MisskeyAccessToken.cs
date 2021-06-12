using System;

namespace KoharuYomiageApp.Domain.Account.Misskey
{
    public record MisskeyAccessToken
    {
        public MisskeyAccessToken(string token)
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
