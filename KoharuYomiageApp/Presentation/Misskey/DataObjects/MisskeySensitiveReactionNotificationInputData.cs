
namespace KoharuYomiageApp.Presentation.Misskey.DataObjects
{
    public record MisskeySensitiveReactionNotificationInputData(string Username, string Instance, string AuthorDisplayName, string AuthorUsername,
        string Content, string Cw, string ReactionUserDisplayName, string ReactionUsername, string Reaction);
}
