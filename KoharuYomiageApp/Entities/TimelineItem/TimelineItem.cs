using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.ReadingText;

namespace KoharuYomiageApp.Entities.TimelineItem
{
    public abstract class TimelineItem
    {
        public AccountIdentifier AccountIdentifier { get; }

        public TimelineItem(AccountIdentifier accountIdentifier)
        {
            AccountIdentifier = accountIdentifier;
        }

        public abstract ReadingTextItem ConvertToReadingText();
    }
}
