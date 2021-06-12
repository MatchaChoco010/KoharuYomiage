namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeyFollowRequestAcceptNotificationData(string Username, string Instance, string FollowUserDisplayName,
        string FollowUsername);
}
