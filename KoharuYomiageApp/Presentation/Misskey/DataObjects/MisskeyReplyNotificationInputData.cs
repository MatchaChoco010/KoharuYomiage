namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeyReplyNotificationInputData(string Username, string Instance, string ReplyUserDisplayName,
        string ReplyUsername, string Reply);
}
