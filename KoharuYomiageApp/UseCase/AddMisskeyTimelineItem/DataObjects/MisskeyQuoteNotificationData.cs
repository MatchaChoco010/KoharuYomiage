namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeyQuoteNotificationData(string Username, string Instance,
        string QuoteUserDisplayName, string QuoteUsername, string QuoteContent);
}
