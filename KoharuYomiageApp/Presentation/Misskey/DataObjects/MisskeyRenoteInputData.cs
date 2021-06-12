namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeyRenoteInputData(string Username, string Instance, string AuthorDisplayName, string AuthorUsername,
        string RenoteUserDisplayName, string RenoteUsername, string Content);
}
