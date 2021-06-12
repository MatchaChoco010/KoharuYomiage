namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeySensitiveMentionNotificationData(string Username, string Instance, string MentionUserDisplayName,
        string MentionUsername, string Mention, string Cw);
}
