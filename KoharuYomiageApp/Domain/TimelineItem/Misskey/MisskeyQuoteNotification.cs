using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeyQuoteNotification : TimelineItem
    {
        public MisskeyQuoteNotification(AccountIdentifier accountIdentifier, string quoteUserDisplayName, string quoteUsername, string quoteContent) : base(accountIdentifier)
        {
            QuoteUserDisplayName = quoteUserDisplayName;
            QuoteUsername = quoteUsername;
            QuoteContent = quoteContent;
        }

        public string QuoteUserDisplayName { get; }
        public string QuoteUsername { get; }
        public string QuoteContent { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{QuoteUserDisplayName}さんに引用リノートされたよ！\n{QuoteUserDisplayName}さんの投稿\n{QuoteContent}";

            return new ReadingTextItem.MisskeyQuoteNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
