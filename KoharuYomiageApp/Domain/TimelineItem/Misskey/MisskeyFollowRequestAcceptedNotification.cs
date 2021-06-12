using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeyFollowRequestAcceptedNotification : TimelineItem
    {
        public MisskeyFollowRequestAcceptedNotification(AccountIdentifier accountIdentifier, string followUserDisplayName, string followUsername) : base(accountIdentifier)
        {
            FollowUserDisplayName = followUserDisplayName;
            FollowUsername = followUsername;
        }

        public string FollowUserDisplayName { get; }
        public string FollowUsername { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{FollowUserDisplayName}さんにフォローリクエストが承認されたよ！";

            return new ReadingTextItem.MisskeyFollowRequestAcceptedNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
