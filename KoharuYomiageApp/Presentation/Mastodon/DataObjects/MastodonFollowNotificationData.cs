namespace KoharuYomiageApp.Presentation.Mastodon.DataObjects
{
    public record MastodonFollowNotificationData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername);
}
