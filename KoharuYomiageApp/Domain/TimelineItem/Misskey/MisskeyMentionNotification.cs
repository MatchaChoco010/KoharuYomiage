using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeyMentionNotification : TimelineItem
    {
        public MisskeyMentionNotification(AccountIdentifier accountIdentifier, string mentionUserDisplayName,
            string mentionUsername, string mention) : base(accountIdentifier)
        {
            MentionUserDisplayName = mentionUserDisplayName;
            MentionUsername = mentionUsername;
            Mention = mention;
        }

        public string MentionUserDisplayName { get; }
        public string MentionUsername { get; }
        public string Mention { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{MentionUserDisplayName}さんにメンションされたよ！\n{Mention}";

            return new ReadingTextItem.MisskeyMentionNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
