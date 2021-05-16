﻿using System.Collections.Generic;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.ReadingText;

namespace KoharuYomiageApp.Entities.TimelineItem.Mastodon
{
    public class MastodonSensitiveStatus : TimelineItem
    {
        public MastodonSensitiveStatus(AccountIdentifier accountIdentifier, string authorDisplayName,
            string authorUsername, string spoilerText, string content, IEnumerable<string>? mediaDescriptions = null)
            : base(accountIdentifier)
        {
            AuthorDisplayName = authorDisplayName;
            AuthorUsername = authorUsername;
            SpoilerText = spoilerText;
            Content = content;
            MediaDescriptions = mediaDescriptions;
        }

        public string AuthorDisplayName { get; }
        public string AuthorUsername { get; }
        public string SpoilerText { get; }
        public string Content { get; }
        public IEnumerable<string>? MediaDescriptions { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{AuthorDisplayName}さんの投稿\n{SpoilerText}\n{Content}";

            if (MediaDescriptions is not null)
            {
                foreach (var description in MediaDescriptions)
                {
                    text += $"\n{description}";
                }
            }

            return new ReadingTextItem.MastodonSensitiveStatusReadingTextItem(AccountIdentifier, text);
        }
    }
}
