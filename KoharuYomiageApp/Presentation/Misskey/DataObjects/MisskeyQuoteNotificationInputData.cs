namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeyQuoteNotificationInputData(string Username, string Instance,
        string QuoteUserDisplayName, string QuoteUsername, string QuoteContent);
}
