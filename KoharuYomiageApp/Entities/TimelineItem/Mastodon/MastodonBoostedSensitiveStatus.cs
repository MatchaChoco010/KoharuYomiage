using System.Collections.Generic;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.ReadingText;

namespace KoharuYomiageApp.Entities.TimelineItem.Mastodon
{
    public class MastodonBoostedSensitiveStatus : TimelineItem
    {
        public MastodonBoostedSensitiveStatus(AccountIdentifier accountIdentifier, string boostedUserDisplayName,
            string boostedUserUserName, string authorDisplayName, string authorUsername, string spoilerText,
            string content, bool muted, IEnumerable<string>? mediaDescriptions = null) : base(accountIdentifier)
        {
            BoostedUserDisplayName = boostedUserDisplayName;
            BoostedUserUserName = boostedUserUserName;
            AuthorDisplayName = authorDisplayName;
            AuthorUsername = authorUsername;
            SpoilerText = spoilerText;
            Content = content;
            Muted = muted;
            MediaDescriptions = mediaDescriptions;
        }

        public string BoostedUserDisplayName { get; }
        public string BoostedUserUserName { get; }
        public string AuthorDisplayName { get; }
        public string AuthorUsername { get; }
        public string SpoilerText { get; }
        public string Content { get; }
        public bool Muted { get; }
        public IEnumerable<string>? MediaDescriptions { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{BoostedUserDisplayName}さんがブースト\n{SpoilerText}\n{AuthorDisplayName}さんの投稿\n{Content}";

            if (MediaDescriptions is not null)
            {
                foreach (var description in MediaDescriptions)
                {
                    text += $"\n{description}";
                }
            }

            return new ReadingTextItem.MastodonBoostedSensitiveStatusReadingTextItem(AccountIdentifier, text);
        }
    }
}
