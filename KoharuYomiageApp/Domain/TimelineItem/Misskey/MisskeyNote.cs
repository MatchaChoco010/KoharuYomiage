using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeyNote : TimelineItem
    {
        public MisskeyNote(AccountIdentifier accountIdentifier, string authorDisplayName, string authorUsername,
            string content) : base(accountIdentifier)
        {
            AuthorDisplayName = authorDisplayName;
            AuthorUsername = authorUsername;
            Content = content;
        }

        public string AuthorDisplayName { get; }
        public string AuthorUsername { get; }
        public string Content { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{AuthorDisplayName}さんの投稿\n{Content}";

            return new ReadingTextItem.MisskeyNoteReadingTextItem(AccountIdentifier, text);
        }
    }
}
