namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeySensitiveQuoteNotificationInputData(string Username, string Instance,
        string QuoteUserDisplayName, string QuoteUsername, string QuoteContent, string Cw);
}
