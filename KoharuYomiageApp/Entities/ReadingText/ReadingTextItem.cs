using System;
using KoharuYomiageApp.Entities.Account;

namespace KoharuYomiageApp.Entities.ReadingText
{
    public abstract record ReadingTextItem(Guid Id, AccountIdentifier AccountIdentifier, string Text)
    {
        ReadingTextItem(AccountIdentifier accountIdentifier, string text) : this(new Guid(), accountIdentifier, text)
        {
        }

        public bool SameIdentityAs(ReadingTextItem other)
        {
            return Id == other.Id;
        }

        public record MastodonStatusReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MastodonSensitiveStatusReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);
    }
}
