using System.Collections.Generic;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Mastodon
{
    public class MastodonFollowRequestNotification : TimelineItem
    {
        public MastodonFollowRequestNotification(AccountIdentifier accountIdentifier, string authorDisplayName, string authorUsername) : base(accountIdentifier)
        {
            AuthorDisplayName = authorDisplayName;
            AuthorUsername = authorUsername;
        }

        public string AuthorDisplayName { get; }
        public string AuthorUsername { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{AuthorDisplayName}さんからフォローリクエストされたよ！";

            return new ReadingTextItem.MastodonFollowRequestNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
