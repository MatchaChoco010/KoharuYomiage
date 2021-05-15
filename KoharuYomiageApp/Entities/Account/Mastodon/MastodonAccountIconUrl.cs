using System;

namespace KoharuYomiageApp.Entities.Account.Mastodon
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
