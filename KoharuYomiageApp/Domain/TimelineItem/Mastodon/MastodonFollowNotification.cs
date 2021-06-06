using System.Collections.Generic;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Mastodon
{
    public class MastodonFollowNotification : TimelineItem
    {
        public MastodonFollowNotification(AccountIdentifier accountIdentifier, string authorDisplayName, string authorUsername) : base(accountIdentifier)
        {
            AuthorDisplayName = authorDisplayName;
            AuthorUsername = authorUsername;
        }

        public string AuthorDisplayName { get; }
        public string AuthorUsername { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{AuthorDisplayName}さんからフォローされたよ！";

            return new ReadingTextItem.MastodonFollowNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
