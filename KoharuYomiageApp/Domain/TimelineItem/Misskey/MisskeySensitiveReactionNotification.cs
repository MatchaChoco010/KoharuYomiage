using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Misskey
{
    public class MisskeySensitiveReactionNotification : TimelineItem
    {
        public MisskeySensitiveReactionNotification(AccountIdentifier accountIdentifier, string authorDisplayName, string authorUsername,
            string content, string cw, string reactionUserDisplayName, string reactionUsername, string reaction) : base(accountIdentifier)
        {
            AuthorDisplayName = authorDisplayName;
            AuthorUsername = authorUsername;
            Content = content;
            Cw = cw;
            ReactionUserDisplayName = reactionUserDisplayName;
            ReactionUsername = reactionUsername;
            Reaction = reaction;
        }

        public string AuthorDisplayName { get; }
        public string AuthorUsername { get; }
        public string Content { get; }
        public string Cw { get; }
        public string ReactionUserDisplayName { get; }
        public string ReactionUsername { get; }
        public string Reaction { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{ReactionUserDisplayName}さんが{Reaction}でリアクションしたよ！\n{AuthorDisplayName}さんの投稿\n{Cw}\n{Content}";

            return new ReadingTextItem.MisskeySensitiveReactionNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
