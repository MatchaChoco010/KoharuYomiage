using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.ReadingText;

namespace KoharuYomiageApp.Entities.TimelineItem
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
