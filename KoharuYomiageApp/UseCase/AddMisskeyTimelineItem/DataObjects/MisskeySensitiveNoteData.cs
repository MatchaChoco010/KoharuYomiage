namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeySensitiveNoteData(string Username, string Instance, string AuthorDisplayName, string AuthorUsername,
        string Content, string Cw);
}
