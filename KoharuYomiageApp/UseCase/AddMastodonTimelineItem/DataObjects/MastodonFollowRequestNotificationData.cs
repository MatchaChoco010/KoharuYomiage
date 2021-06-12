namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects
{
    public record MastodonFollowRequestNotificationData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername);
}
