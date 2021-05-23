using System;

namespace KoharuYomiageApp.Domain.Account.Mastodon
{
    public record MastodonAccountIconUrl
    {
        public MastodonAccountIconUrl(Uri iconUrl)
        {
            IconUrl = iconUrl;
        }

        public Uri IconUrl { get; }
    }
}
