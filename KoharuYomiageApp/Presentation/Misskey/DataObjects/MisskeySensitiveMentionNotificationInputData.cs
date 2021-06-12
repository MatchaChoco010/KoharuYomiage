namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeySensitiveMentionNotificationInputData(string Username, string Instance, string MentionUserDisplayName,
        string MentionUsername, string Mention, string Cw);
}
