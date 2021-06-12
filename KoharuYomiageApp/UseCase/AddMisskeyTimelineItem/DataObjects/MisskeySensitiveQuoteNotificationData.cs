namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeySensitiveQuoteNotificationData(string Username, string Instance,
        string QuoteUserDisplayName, string QuoteUsername, string QuoteContent, string Cw);
}
