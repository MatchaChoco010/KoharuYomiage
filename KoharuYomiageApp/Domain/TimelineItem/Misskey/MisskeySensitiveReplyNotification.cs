using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeySensitiveReplyNotification : TimelineItem
    {
        public MisskeySensitiveReplyNotification(AccountIdentifier accountIdentifier, string replyUserDisplayName,
            string replyUsername, string reply, string cw) : base(accountIdentifier)
        {
            ReplyUserDisplayName = replyUserDisplayName;
            ReplyUsername = replyUsername;
            Reply = reply;
            Cw = cw;
        }

        public string ReplyUserDisplayName { get; }
        public string ReplyUsername { get; }
        public string Reply { get; }
        public string Cw { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{ReplyUserDisplayName}さんにリプライされたよ！\n{Cw}\n{Reply}";

            return new ReadingTextItem.MisskeySensitiveReplyNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
