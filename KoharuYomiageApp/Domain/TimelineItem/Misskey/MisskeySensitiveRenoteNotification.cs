using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeySensitiveRenoteNotification : TimelineItem
    {
        public MisskeySensitiveRenoteNotification(AccountIdentifier accountIdentifier, string authorDisplayName, string authorUsername,
            string content, string cw, string renoteUserDisplayName, string renoteUsername) : base(accountIdentifier)
        {
            AuthorDisplayName = authorDisplayName;
            AuthorUsername = authorUsername;
            Content = content;
            Cw = cw;
            RenoteUserDisplayName = renoteUserDisplayName;
            RenoteUsername = renoteUsername;
        }

        public string AuthorDisplayName { get; }
        public string AuthorUsername { get; }
        public string Content { get; }
        public string Cw { get; }
        public string RenoteUserDisplayName { get; }
        public string RenoteUsername { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{RenoteUserDisplayName}さんにリノートされたよ！\n{AuthorDisplayName}さんの投稿\n{Cw}\n{Content}";

            return new ReadingTextItem.MisskeySensitiveRenoteNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
