namespace KoharuYomiageApp.Presentation.Mastodon.DataObjects
{
    public record MastodonFollowNotificationInputData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername);
}
