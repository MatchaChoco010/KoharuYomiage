using System;

namespace KoharuYomiageApp.Domain.Account
{
    public record AccountIconUrl
    {
        public AccountIconUrl(Uri iconUrl)
        {
            IconUrl = iconUrl;
        }

        public Uri IconUrl { get; }
    }
}
