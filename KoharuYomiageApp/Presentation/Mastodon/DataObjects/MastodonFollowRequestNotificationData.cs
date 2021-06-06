namespace KoharuYomiageApp.Presentation.Mastodon.DataObjects
{
    public record MastodonFollowRequestNotificationData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername);
}
