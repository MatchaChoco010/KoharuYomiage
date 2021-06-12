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

        public record MisskeyNoteReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeySensitiveNoteReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeyRenoteReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeySensitiveRenoteReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeyReactionNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeySensitiveReactionNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeyReplyNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeySensitiveReplyNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeyRenoteNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeySensitiveRenoteNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeyQuoteNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeySensitiveQuoteNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeyMentionNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeySensitiveMentionNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeyFollowNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeyFollowRequestAcceptedNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);

        public record MisskeyReceiveFollowRequestNotificationReadingTextItem(AccountIdentifier AccountIdentifier, string Text)
            : ReadingTextItem(AccountIdentifier, Text);
    }
}
