
namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeySensitiveReactionNotificationData(string Username, string Instance, string AuthorDisplayName, string AuthorUsername,
        string Content, string Cw, string ReactionUserDisplayName, string ReactionUsername, string Reaction);
}
