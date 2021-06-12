using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.TimelineItem.Misskey;
using KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem
{
    public class MisskeyTimelineItemReceiver : IMisskeyTimelineItemReceiver
    {
        readonly IReadingTextContainerRepository _readingTextContainerRepository;

        public MisskeyTimelineItemReceiver(IReadingTextContainerRepository readingTextContainerRepository)
        {
            _readingTextContainerRepository = readingTextContainerRepository;
        }

        public void ReceiveNote(MisskeyNoteData data)
        {
            var note = new MisskeyNote(new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.AuthorDisplayName, data.AuthorUsername, data.Content);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(note.ConvertToReadingText());
        }

        public void ReceiveSensitiveNote(MisskeySensitiveNoteData data)
        {
            var note = new MisskeySensitiveNote(new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.AuthorDisplayName, data.AuthorUsername, data.Content, data.Cw);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(note.ConvertToReadingText());
        }

        public void ReceiveRenote(MisskeyRenoteData data)
        {
            var renote = new MisskeyRenote(new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.AuthorDisplayName, data.AuthorUsername, data.RenoteUserDisplayName, data.RenoteUsername, data.Content);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(renote.ConvertToReadingText());
        }

        public void ReceiveSensitiveRenote(MisskeySensitiveRenoteData data)
        {
            var renote = new MisskeySensitiveRenote(new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.AuthorDisplayName, data.AuthorUsername, data.RenoteUserDisplayName, data.RenoteUsername, data.Content, data.Cw);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(renote.ConvertToReadingText());
        }

        public void ReceiveReactionNotification(MisskeyReactionNotificationData data)
        {
            var notification = new MisskeyReactionNotification(new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.AuthorDisplayName, data.AuthorUsername, data.Content, data.ReactionUserDisplayName, data.ReactionUsername,
                data.Reaction);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }

        public void ReceiveSensitiveReactionNotification(MisskeySensitiveReactionNotificationData data)
        {
            var notification = new MisskeySensitiveReactionNotification(new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.AuthorDisplayName, data.AuthorUsername, data.Content, data.Cw, data.ReactionUserDisplayName, data.ReactionUsername,
                data.Reaction);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }

        public void ReceiveReplyNotification(MisskeyReplyNotificationData data)
        {
            var notification = new MisskeyReplyNotification(new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.ReplyUserDisplayName, data.ReplyUsername, data.Reply);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }

        public void ReceiveSensitiveReplyNotification(MisskeySensitiveReplyNotificationData data)
        {
            var notification = new MisskeySensitiveReplyNotification(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.ReplyUserDisplayName, data.ReplyUsername, data.Reply, data.Cw);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }

        public void ReceiveRenoteNotification(MisskeyRenoteNotificationData data)
        {
            var notification = new MisskeyRenoteNotification(new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.AuthorDisplayName, data.AuthorUsername ,data.RenoteUserDisplayName, data.RenoteUsername, data.Content);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }

        public void ReceiveSensitiveRenoteNotification(MisskeySensitiveRenoteNotificationData data)
        {
            var notification = new MisskeySensitiveRenoteNotification(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.AuthorDisplayName, data.AuthorUsername ,data.RenoteUserDisplayName, data.RenoteUsername, data.Content, data.Cw);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }

        public void ReceiveQuoteNotification(MisskeyQuoteNotificationData data)
        {
            var notification = new MisskeyQuoteNotification(new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.QuoteUserDisplayName, data.QuoteUsername, data.QuoteContent);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }

        public void ReceiveSensitiveQuoteNotification(MisskeySensitiveQuoteNotificationData data)
        {
            var notification = new MisskeySensitiveQuoteNotification(new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.QuoteUserDisplayName, data.QuoteUsername, data.QuoteContent, data.Cw);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }

        public void ReceiveMentionNotification(MisskeyMentionNotificationData data)
        {
            var notification = new MisskeyMentionNotification(new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.MentionUserDisplayName, data.MentionUsername, data.Mention);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }

        public void ReceiveSensitiveMentionNotification(MisskeySensitiveMentionNotificationData data)
        {
            var notification = new MisskeySensitiveMentionNotification(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.MentionUserDisplayName, data.MentionUsername, data.Mention, data.Cw);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }

        public void ReceiveFollowNotification(MisskeyFollowNotificationData data)
        {
            var notification = new MisskeyFollowNotification(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.FollowUserDisplayName, data.FollowUsername);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }

        public void ReceiveFollowRequestAcceptNotification(MisskeyFollowRequestAcceptNotificationData data)
        {
            var notification = new MisskeyFollowRequestAcceptedNotification(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.FollowUserDisplayName, data.FollowUsername);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }

        public void ReceiveReceiveFollowRequestNotification(MisskeyReceiveFollowRequestNotificationData data)
        {
            var notification = new MisskeyReceiveFollowRequestNotification(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.FollowUserDisplayName, data.FollowUsername);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(notification.ConvertToReadingText());
        }
    }
}
