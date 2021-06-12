namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeySensitiveReplyNotificationData(string Username, string Instance, string ReplyUserDisplayName,
        string ReplyUsername, string Reply, string Cw);
}
