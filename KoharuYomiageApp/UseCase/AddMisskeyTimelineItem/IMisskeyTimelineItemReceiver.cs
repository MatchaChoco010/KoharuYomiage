using System.Threading;
using KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem
{
    public interface IMisskeyTimelineItemReceiver
    {
        void ReceiveNote(MisskeyNoteData data);
        void ReceiveSensitiveNote(MisskeySensitiveNoteData data);
        void ReceiveRenote(MisskeyRenoteData data);
        void ReceiveSensitiveRenote(MisskeySensitiveRenoteData data);
        void ReceiveReactionNotification(MisskeyReactionNotificationData data);
        void ReceiveSensitiveReactionNotification(MisskeySensitiveReactionNotificationData data);
        void ReceiveReplyNotification(MisskeyReplyNotificationData data);
        void ReceiveSensitiveReplyNotification(MisskeySensitiveReplyNotificationData data);
        void ReceiveRenoteNotification(MisskeyRenoteNotificationData data);
        void ReceiveSensitiveRenoteNotification(MisskeySensitiveRenoteNotificationData data);
        void ReceiveQuoteNotification(MisskeyQuoteNotificationData data);
        void ReceiveSensitiveQuoteNotification(MisskeySensitiveQuoteNotificationData data);
        void ReceiveMentionNotification(MisskeyMentionNotificationData data);
        void ReceiveSensitiveMentionNotification(MisskeySensitiveMentionNotificationData data);
        void ReceiveFollowNotification(MisskeyFollowNotificationData data);
        void ReceiveFollowRequestAcceptNotification(MisskeyFollowRequestAcceptNotificationData data);
        void ReceiveReceiveFollowRequestNotification(MisskeyReceiveFollowRequestNotificationData data);
    }
}
