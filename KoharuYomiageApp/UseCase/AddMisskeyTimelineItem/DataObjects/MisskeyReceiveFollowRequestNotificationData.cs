namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeyReceiveFollowRequestNotificationData(string Username, string Instance, string FollowUserDisplayName,
        string FollowUsername);
}
