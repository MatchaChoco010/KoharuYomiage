using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeySensitiveRenote : TimelineItem
    {
        public MisskeySensitiveRenote(AccountIdentifier accountIdentifier, string authorDisplayName, string authorUsername,
            string renoteUserDisplayName, string renoteUsername, string content, string cw) : base(accountIdentifier)
        {
            AuthorDisplayName = authorDisplayName;
            AuthorUsername = authorUsername;
            RenoteUserDisplayName = renoteUserDisplayName;
            RenoteUsername = renoteUsername;
            Content = content;
            Cw = cw;
        }

        public string AuthorDisplayName { get; }
        public string AuthorUsername { get; }
        public string RenoteUserDisplayName { get; }
        public string RenoteUsername { get; }
        public string Content { get; }
        public string Cw { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{RenoteUserDisplayName}さんのリノート！\n{AuthorDisplayName}さんの投稿\n{Cw}\n{Content}";

            return new ReadingTextItem.MisskeySensitiveRenoteReadingTextItem(AccountIdentifier, text);
        }
    }
}
