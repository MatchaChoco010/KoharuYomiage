using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeyReceiveFollowRequestNotification : TimelineItem
    {
        public MisskeyReceiveFollowRequestNotification(AccountIdentifier accountIdentifier, string followUserDisplayName, string followUsername) : base(accountIdentifier)
        {
            FollowUserDisplayName = followUserDisplayName;
            FollowUsername = followUsername;
        }

        public string FollowUserDisplayName { get; }
        public string FollowUsername { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{FollowUserDisplayName}さんからフォローリクエストされたよ！";

            return new ReadingTextItem.MisskeyReceiveFollowRequestNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
