namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeyFollowRequestAcceptNotificationInputData(string Username, string Instance, string FollowUserDisplayName,
        string FollowUsername);
}
