using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeySensitiveQuoteNotification : TimelineItem
    {
        public MisskeySensitiveQuoteNotification(AccountIdentifier accountIdentifier, string quoteUserDisplayName, string quoteUsername, string quoteContent, string quoteCw) : base(accountIdentifier)
        {
            QuoteUserDisplayName = quoteUserDisplayName;
            QuoteUsername = quoteUsername;
            QuoteContent = quoteContent;
            QuoteCw = quoteCw;
        }

        public string QuoteUserDisplayName { get; }
        public string QuoteUsername { get; }
        public string QuoteContent { get; }
        public string QuoteCw { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{QuoteUserDisplayName}さんに引用リノートされたよ！\n{QuoteUserDisplayName}さんの投稿\n{QuoteCw}\n{QuoteContent}";

            return new ReadingTextItem.MisskeySensitiveQuoteNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
