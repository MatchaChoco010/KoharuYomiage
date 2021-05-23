using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem
{
    public abstract class TimelineItem
    {
        public TimelineItem(AccountIdentifier accountIdentifier)
        {
            AccountIdentifier = accountIdentifier;
        }

        public AccountIdentifier AccountIdentifier { get; }

        public abstract ReadingTextItem ConvertToReadingText();
    }
}
