using System;

namespace KoharuYomiageApp.Entities.Client.Mastodon
{
    public record MastodonClientId
    {
        public MastodonClientId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Mastodon client id must not be empty");
            }

            Value = value;
        }

        public string Value { get; }
    }
}
