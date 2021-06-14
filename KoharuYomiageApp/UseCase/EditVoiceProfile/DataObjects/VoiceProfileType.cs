namespace KoharuYomiageApp.UseCase.EditVoiceProfile.DataObjects
{
    public enum VoiceProfileType
    {
        MastodonStatus,
        MastodonSensitiveStatus,
        MastodonBoostedStatus,
        MastodonBoostedSensitiveStatus,
        MastodonFollowNotification,
        MastodonFollowRequestNotification,
        MastodonMentionNotification,
        MastodonSensitiveMentionNotification,
        MastodonReblogNotification,
        MastodonSensitiveReblogNotification,
        MastodonFavoriteNotification,
        MastodonSensitiveFavoriteNotification,

        MisskeyNote,
        MisskeySensitiveNote,
        MisskeyRenote,
        MisskeySensitiveRenote,
        MisskeyReactionNotification,
        MisskeySensitiveReactionNotification,
        MisskeyReplyNotification,
        MisskeySensitiveReplyNotification,
        MisskeyRenoteNotification,
        MisskeySensitiveRenoteNotification,
        MisskeyQuoteNotification,
        MisskeySensitiveQuoteNotification,
        MisskeyMentionNotification,
        MisskeySensitiveMentionNotification,
        MisskeyFollowNotification,
        MisskeyFollowRequestAcceptedNotification,
        MisskeyReceiveFollowRequestNotification,
    }
}
