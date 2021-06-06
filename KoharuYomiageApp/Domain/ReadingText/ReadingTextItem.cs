using System;
using KoharuYomiageApp.Domain.Account;

namespace KoharuYomiageApp.Domain.ReadingText
{
    public abstract record ReadingTextItem(Guid Id, AccountIdentifier AccountIdentifier, string Text)
    {
        ReadingTextItem(AccountIdentifier accountIdentifier, string text) : this(Guid.NewGuid(), accountIdentifier,
            text)
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

        public record MastodonBoostedStatusReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MastodonFollowNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MastodonBoostedSensitiveStatusReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MastodonFollowRequestNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MastodonMentionNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MastodonSensitiveMentionNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MastodonReblogNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MastodonSensitiveReblogNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MastodonFavoriteNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MastodonSensitiveFavoriteNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);
    }
}
