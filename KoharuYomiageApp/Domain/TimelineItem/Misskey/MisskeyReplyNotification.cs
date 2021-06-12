using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeyReplyNotification : TimelineItem
    {
        public MisskeyReplyNotification(AccountIdentifier accountIdentifier, string replyUserDisplayName,
            string replyUsername, string reply) : base(accountIdentifier)
        {
            ReplyUserDisplayName = replyUserDisplayName;
            ReplyUsername = replyUsername;
            Reply = reply;
        }

        public string ReplyUserDisplayName { get; }
        public string ReplyUsername { get; }
        public string Reply { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{ReplyUserDisplayName}さんにリプライされたよ！\n{Reply}";

            return new ReadingTextItem.MisskeyReplyNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
