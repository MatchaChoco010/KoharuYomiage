namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeyNoteInputData(string Username, string Instance, string AuthorDisplayName, string AuthorUsername,
        string Content);
}
