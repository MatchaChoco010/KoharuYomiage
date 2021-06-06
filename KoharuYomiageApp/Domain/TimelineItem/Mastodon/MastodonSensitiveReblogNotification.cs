using System.Collections.Generic;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Mastodon
{
    public class MastodonSensitiveReblogNotification : TimelineItem
    {
        public MastodonSensitiveReblogNotification(AccountIdentifier accountIdentifier, string displayName,
            string reblogUserDisplayName, string reblogUserUsername, string spoilerText, string content,
            IEnumerable<string>? mediaDescriptions = null) : base(accountIdentifier)
        {
            DisplayName = displayName;
            ReblogUserDisplayName = reblogUserDisplayName;
            ReblogUserUsername = reblogUserUsername;
            SpoilerText = spoilerText;
            Content = content;
            MediaDescriptions = mediaDescriptions;
        }

        public string DisplayName { get; }
        public string ReblogUserDisplayName { get; }
        public string ReblogUserUsername { get; }
        public string SpoilerText { get; }
        public string Content { get; }
        public IEnumerable<string>? MediaDescriptions { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{ReblogUserDisplayName}さんからのブーストだよ！\n{DisplayName}さんの投稿\n{SpoilerText}\n{Content}";

            if (MediaDescriptions is not null)
            {
                foreach (var description in MediaDescriptions)
                {
                    text += $"\n{description}";
                }
            }

            return new ReadingTextItem.MastodonSensitiveReblogNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
