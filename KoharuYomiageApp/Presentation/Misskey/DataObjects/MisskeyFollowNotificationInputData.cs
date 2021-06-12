namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeyFollowNotificationInputData(string Username, string Instance, string FollowUserDisplayName,
        string FollowUsername);
}
