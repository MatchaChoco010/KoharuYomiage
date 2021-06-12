using KoharuYomiageApp.Presentation.Misskey.DataObjects;
using KoharuYomiageApp.UseCase.AddMisskeyTimelineItem;
using KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects;

namespace KoharuYomiageApp.Presentation.Misskey
{
    public class MisskeyController
    {
        readonly IMisskeyTimelineItemReceiver _itemReceiver;

        public MisskeyController(IMisskeyTimelineItemReceiver itemReceiver)
        {
            _itemReceiver = itemReceiver;
        }

        public void AddNote(MisskeyNoteInputData inputData)
        {
            _itemReceiver.ReceiveNote(new MisskeyNoteData(inputData.Username, inputData.Instance,
                inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.Content));
        }

        public void AddSensitiveNote(MisskeySensitiveNoteInputData inputData)
        {
            _itemReceiver.ReceiveSensitiveNote(new MisskeySensitiveNoteData(inputData.Username, inputData.Instance,
                inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.Content, inputData.Cw));
        }

        public void AddRenote(MisskeyRenoteInputData inputData)
        {
            _itemReceiver.ReceiveRenote(new MisskeyRenoteData(inputData.Username, inputData.Instance,
                inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.RenoteUserDisplayName,
                inputData.RenoteUsername, inputData.Content));
        }

        public void AddSensitiveRenote(MisskeySensitiveRenoteInputData inputData)
        {
            _itemReceiver.ReceiveSensitiveRenote(new MisskeySensitiveRenoteData(inputData.Username, inputData.Instance,
                inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.RenoteUserDisplayName,
                inputData.RenoteUsername, inputData.Content, inputData.Cw));
        }

        public void AddReactionNotification(MisskeyReactionNotificationInputData inputData)
        {
            _itemReceiver.ReceiveReactionNotification(new MisskeyReactionNotificationData(inputData.Username,
                inputData.Instance, inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.Content,
                inputData.ReactionUserDisplayName, inputData.ReactionUsername, inputData.Reaction));
        }

        public void AddSensitiveReactionNotification(MisskeySensitiveReactionNotificationInputData inputData)
        {
            _itemReceiver.ReceiveSensitiveReactionNotification(new MisskeySensitiveReactionNotificationData(
                inputData.Username, inputData.Instance, inputData.AuthorDisplayName, inputData.AuthorUsername,
                inputData.Content, inputData.Cw, inputData.ReactionUserDisplayName, inputData.ReactionUsername,
                inputData.Reaction));
        }

        public void AddReplyNotification(MisskeyReplyNotificationInputData inputData)
        {
            _itemReceiver.ReceiveReplyNotification(new MisskeyReplyNotificationData(inputData.Username,
                inputData.Instance, inputData.ReplyUserDisplayName, inputData.ReplyUsername, inputData.Reply));
        }

        public void AddSensitiveReplyNotification(MisskeySensitiveReplyNotificationInputData inputData)
        {
            _itemReceiver.ReceiveSensitiveReplyNotification(new MisskeySensitiveReplyNotificationData(
                inputData.Username, inputData.Instance, inputData.ReplyUserDisplayName, inputData.ReplyUsername,
                inputData.Reply, inputData.Cw));
        }

        public void AddRenoteNotification(MisskeyRenoteNotificationInputData inputData)
        {
            _itemReceiver.ReceiveRenoteNotification(new MisskeyRenoteNotificationData(
                inputData.Username, inputData.Instance, inputData.AuthorDisplayName, inputData.AuthorUsername,
                inputData.RenoteUserDisplayName, inputData.RenoteUsername, inputData.Content));
        }

        public void AddSensitiveRenoteNotification(MisskeySensitiveRenoteNotificationInputData inputData)
        {
            _itemReceiver.ReceiveSensitiveRenoteNotification(new MisskeySensitiveRenoteNotificationData(
                inputData.Username, inputData.Instance, inputData.AuthorDisplayName, inputData.AuthorUsername,
                inputData.RenoteUserDisplayName, inputData.RenoteUsername, inputData.Content, inputData.Cw));
        }

        public void AddQuoteNotification(MisskeyQuoteNotificationInputData inputData)
        {
            _itemReceiver.ReceiveQuoteNotification(new MisskeyQuoteNotificationData(inputData.Username,
                inputData.Instance, inputData.QuoteUserDisplayName, inputData.QuoteUsername, inputData.QuoteContent));
        }

        public void AddSensitiveQuoteNotification(MisskeySensitiveQuoteNotificationInputData inputData)
        {
            _itemReceiver.ReceiveSensitiveQuoteNotification(new MisskeySensitiveQuoteNotificationData(
                inputData.Username, inputData.Instance, inputData.QuoteUserDisplayName, inputData.QuoteUsername,
                inputData.QuoteContent, inputData.Cw));
        }

        public void AddMentionNotification(MisskeyMentionNotificationInputData inputData)
        {
            _itemReceiver.ReceiveMentionNotification(new MisskeyMentionNotificationData(inputData.Username,
                inputData.Instance, inputData.MentionUserDisplayName, inputData.MentionUsername, inputData.Mention));
        }

        public void AddSensitiveMentionNotification(MisskeySensitiveMentionNotificationInputData inputData)
        {
            _itemReceiver.ReceiveSensitiveMentionNotification(new MisskeySensitiveMentionNotificationData(
                inputData.Username, inputData.Instance, inputData.MentionUserDisplayName, inputData.MentionUsername,
                inputData.Mention, inputData.Cw));
        }

        public void AddFollowNotification(MisskeyFollowNotificationInputData inputData)
        {
            _itemReceiver.ReceiveFollowNotification(new MisskeyFollowNotificationData(inputData.Username,
                inputData.Instance, inputData.FollowUserDisplayName, inputData.FollowUsername));
        }

        public void AddFollowRequestAcceptNotification(MisskeyFollowRequestAcceptNotificationInputData inputData)
        {
            _itemReceiver.ReceiveFollowRequestAcceptNotification(new MisskeyFollowRequestAcceptNotificationData(
                inputData.Username, inputData.Instance, inputData.FollowUserDisplayName, inputData.FollowUsername));
        }

        public void AddReceiveFollowRequestNotification(MisskeyReceiveFollowRequestNotificationInputData inputData)
        {
            _itemReceiver.ReceiveReceiveFollowRequestNotification(new MisskeyReceiveFollowRequestNotificationData(
                inputData.Username, inputData.Instance, inputData.FollowUserDisplayName, inputData.FollowUsername));
        }
    }
}
