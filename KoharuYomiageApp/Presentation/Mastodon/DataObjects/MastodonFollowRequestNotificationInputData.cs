namespace KoharuYomiageApp.Presentation.Mastodon.DataObjects
{
    public record MastodonFollowRequestNotificationInputData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername);
}
