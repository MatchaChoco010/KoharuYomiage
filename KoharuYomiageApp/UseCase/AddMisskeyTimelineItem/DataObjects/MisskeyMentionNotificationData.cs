namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeyMentionNotificationData(string Username, string Instance, string MentionUserDisplayName,
        string MentionUsername, string Mention);
}
