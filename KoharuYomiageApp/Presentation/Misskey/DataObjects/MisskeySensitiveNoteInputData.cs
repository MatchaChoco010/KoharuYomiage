namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeySensitiveNoteInputData(string Username, string Instance, string AuthorDisplayName, string AuthorUsername,
        string Content, string Cw);
}
