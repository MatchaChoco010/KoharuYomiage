namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeyReplyNotificationData(string Username, string Instance, string ReplyUserDisplayName,
        string ReplyUsername, string Reply);
}
