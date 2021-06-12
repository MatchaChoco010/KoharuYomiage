using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeySensitiveNote : TimelineItem
    {
        public MisskeySensitiveNote(AccountIdentifier accountIdentifier, string authorDisplayName, string authorUsername,
            string content, string cw) : base(accountIdentifier)
        {
            AuthorDisplayName = authorDisplayName;
            AuthorUsername = authorUsername;
            Content = content;
            Cw = cw;
        }

        public string AuthorDisplayName { get; }
        public string AuthorUsername { get; }
        public string Content { get; }
        public string Cw { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{AuthorDisplayName}さんの投稿\n{Cw}\n{Content}";

            return new ReadingTextItem.MisskeySensitiveNoteReadingTextItem(AccountIdentifier, text);
        }
    }
}
