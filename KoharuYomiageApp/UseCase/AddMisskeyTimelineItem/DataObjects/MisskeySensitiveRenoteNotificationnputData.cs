namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeySensitiveRenoteNotificationData(string Username, string Instance, string AuthorDisplayName, string AuthorUsername,
        string RenoteUserDisplayName, string RenoteUsername, string Content, string Cw);
}
