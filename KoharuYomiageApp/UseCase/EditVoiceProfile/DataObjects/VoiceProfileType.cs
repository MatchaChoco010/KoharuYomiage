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
    }
}
