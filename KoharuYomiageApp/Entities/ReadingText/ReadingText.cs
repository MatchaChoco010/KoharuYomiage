using System;
using KoharuYomiageApp.Entities.Account;

namespace KoharuYomiageApp.Entities
{
    public abstract record ReadingText(Guid Id, AccountIdentifier AccountIdentifier, string Text)
    {
        ReadingText(AccountIdentifier accountIdentifier, string text) : this(new Guid(), accountIdentifier, text) { }

        public bool SameIdentityAs(ReadingText other)
        {
            return Id == other.Id;
        }

        public record MastodonStatusReadingText(AccountIdentifier AccountIdentifier, string Text)
            : ReadingText(AccountIdentifier, Text);

        public record MastodonSensitiveStatusReadingText(AccountIdentifier AccountIdentifier, string Text)
            : ReadingText(AccountIdentifier, Text);
    }
}
