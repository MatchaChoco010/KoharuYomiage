namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeyMentionNotificationInputData(string Username, string Instance, string MentionUserDisplayName,
        string MentionUsername, string Mention);
}
