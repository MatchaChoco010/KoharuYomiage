namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeySensitiveReplyNotificationInputData(string Username, string Instance, string ReplyUserDisplayName,
        string ReplyUsername, string Reply, string Cw);
}
