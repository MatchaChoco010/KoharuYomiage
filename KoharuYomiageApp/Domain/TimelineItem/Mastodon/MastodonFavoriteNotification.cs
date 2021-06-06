using System.Collections.Generic;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Mastodon
{
    public class MastodonFavoriteNotification : TimelineItem
    {
        public MastodonFavoriteNotification(AccountIdentifier accountIdentifier, string displayName,
            string favoriteUserDisplayName, string favoriteUserUsername, string content,
            IEnumerable<string>? mediaDescriptions = null) : base(accountIdentifier)
        {
            DisplayName = displayName;
            FavoriteUserDisplayName = favoriteUserDisplayName;
            FavoriteUserUsername = favoriteUserUsername;
            Content = content;
            MediaDescriptions = mediaDescriptions;
        }

        public string DisplayName { get; }
        public string FavoriteUserDisplayName { get; }
        public string FavoriteUserUsername { get; }
        public string Content { get; }
        public IEnumerable<string>? MediaDescriptions { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{FavoriteUserDisplayName}さんからのファボだよ！{DisplayName}さんの投稿\n{Content}";

            if (MediaDescriptions is not null)
            {
                foreach (var description in MediaDescriptions)
                {
                    text += $"\n{description}";
                }
            }

            return new ReadingTextItem.MastodonFavoriteNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
