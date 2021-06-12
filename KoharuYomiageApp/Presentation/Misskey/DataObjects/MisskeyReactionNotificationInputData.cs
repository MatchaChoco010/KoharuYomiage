
namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeyReactionNotificationInputData(string Username, string Instance, string AuthorDisplayName, string AuthorUsername,
        string Content, string ReactionUserDisplayName, string ReactionUsername, string Reaction);
}
