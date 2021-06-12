namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeyReceiveFollowRequestNotificationInputData(string Username, string Instance, string FollowUserDisplayName,
        string FollowUsername);
}
