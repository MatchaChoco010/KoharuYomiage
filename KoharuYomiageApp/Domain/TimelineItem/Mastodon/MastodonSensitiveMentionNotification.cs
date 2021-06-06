using System.Collections.Generic;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Mastodon
{
    public class MastodonSensitiveMentionNotification : TimelineItem
    {
        public MastodonSensitiveMentionNotification(AccountIdentifier accountIdentifier, string authorDisplayName,
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
            var text = $"{AuthorDisplayName}さんからメンションだよ！\n{SpoilerText}\n{Content}";

            if (MediaDescriptions is not null)
            {
                foreach (var description in MediaDescriptions)
                {
                    text += $"\n{description}";
                }
            }

            return new ReadingTextItem.MastodonSensitiveMentionNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
