
namespace KoharuYomiageApp.UseCase.AddMisskeyTimelineItem.DataObjects
{
    public record MisskeyReactionNotificationData(string Username, string Instance, string AuthorDisplayName, string AuthorUsername,
        string Content, string ReactionUserDisplayName, string ReactionUsername, string Reaction);
}
