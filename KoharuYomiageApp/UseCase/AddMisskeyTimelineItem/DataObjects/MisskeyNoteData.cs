namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeyNoteData(string Username, string Instance, string AuthorDisplayName, string AuthorUsername,
        string Content);
}
