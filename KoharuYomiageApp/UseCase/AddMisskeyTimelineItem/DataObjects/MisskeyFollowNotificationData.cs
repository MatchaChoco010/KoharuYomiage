namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeyFollowNotificationData(string Username, string Instance, string FollowUserDisplayName,
        string FollowUsername);
}
