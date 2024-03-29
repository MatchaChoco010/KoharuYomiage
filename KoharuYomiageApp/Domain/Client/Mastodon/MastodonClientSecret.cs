﻿using System;

namespace KoharuYomiageApp.Domain.Client.Mastodon
{
    public record MastodonClientSecret
    {
        public MastodonClientSecret(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Client Secret must not be empty");
            }

            Value = value;
        }

        public string Value { get; }
    }
}
