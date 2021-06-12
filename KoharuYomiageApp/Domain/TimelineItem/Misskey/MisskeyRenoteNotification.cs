using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeyRenoteNotification : TimelineItem
    {
        public MisskeyRenoteNotification(AccountIdentifier accountIdentifier, string authorDisplayName, string authorUsername,
            string content, string renoteUserDisplayName, string renoteUsername) : base(accountIdentifier)
        {
            AuthorDisplayName = authorDisplayName;
            AuthorUsername = authorUsername;
            Content = content;
            RenoteUserDisplayName = renoteUserDisplayName;
            RenoteUsername = renoteUsername;
        }

        public string AuthorDisplayName { get; }
        public string AuthorUsername { get; }
        public string Content { get; }
        public string RenoteUserDisplayName { get; }
        public string RenoteUsername { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{RenoteUserDisplayName}さんにリノートされたよ！\n{AuthorDisplayName}さんの投稿\n{Content}";

            return new ReadingTextItem.MisskeyRenoteNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
