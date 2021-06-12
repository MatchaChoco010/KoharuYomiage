namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects
{
    public record MastodonFollowNotificationData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername);
}
