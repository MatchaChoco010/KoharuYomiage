using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeyRenote : TimelineItem
    {
        public MisskeyRenote(AccountIdentifier accountIdentifier, string authorDisplayName, string authorUsername,
            string renoteUserDisplayName, string renoteUsername, string content) : base(accountIdentifier)
        {
            AuthorDisplayName = authorDisplayName;
            AuthorUsername = authorUsername;
            RenoteUserDisplayName = renoteUserDisplayName;
            RenoteUsername = renoteUsername;
            Content = content;
        }

        public string AuthorDisplayName { get; }
        public string AuthorUsername { get; }
        public string RenoteUserDisplayName { get; }
        public string RenoteUsername { get; }
        public string Content { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{RenoteUserDisplayName}さんのリノート！\n{AuthorDisplayName}さんの投稿\n{Content}";

            return new ReadingTextItem.MisskeyRenoteReadingTextItem(AccountIdentifier, text);
        }
    }
}
