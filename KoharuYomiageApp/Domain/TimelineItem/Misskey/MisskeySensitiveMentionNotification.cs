using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeySensitiveMentionNotification : TimelineItem
    {
        public MisskeySensitiveMentionNotification(AccountIdentifier accountIdentifier, string mentionUserDisplayName,
            string mentionUsername, string mention, string cw) : base(accountIdentifier)
        {
            MentionUserDisplayName = mentionUserDisplayName;
            MentionUsername = mentionUsername;
            Mention = mention;
            Cw = cw;
        }

        public string MentionUserDisplayName { get; }
        public string MentionUsername { get; }
        public string Mention { get; }
        public string Cw { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{MentionUserDisplayName}さんにメンションされたよ！\n{Cw}\n{Mention}";

            return new ReadingTextItem.MisskeySensitiveMentionNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
