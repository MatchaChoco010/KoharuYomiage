using System;

namespace KoharuYomiageApp.Entities
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
