using System.Collections.Generic;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.Domain.TimelineItem.Mastodon
{
    public class MastodonSensitiveFavoriteNotification : TimelineItem
    {
        public MastodonSensitiveFavoriteNotification(AccountIdentifier accountIdentifier, string displayName,
            string favoriteUserDisplayName, string favoriteUserUsername, string spoilerText, string content,
            IEnumerable<string>? mediaDescriptions = null) : base(accountIdentifier)
        {
            DisplayName = displayName;
            FavoriteUserDisplayName = favoriteUserDisplayName;
            FavoriteUserUsername = favoriteUserUsername;
            SpoilerText = spoilerText;
            Content = content;
            MediaDescriptions = mediaDescriptions;
        }

        public string DisplayName { get; }
        public string FavoriteUserDisplayName { get; }
        public string FavoriteUserUsername { get; }
        public string SpoilerText { get; }
        public string Content { get; }
        public IEnumerable<string>? MediaDescriptions { get; }

        public override ReadingTextItem ConvertToReadingText()
        {
            var text = $"{FavoriteUserDisplayName}さんからのファボだよ！\n{DisplayName}さんの投稿\n{SpoilerText}\n{Content}";

            if (MediaDescriptions is not null)
            {
                foreach (var description in MediaDescriptions)
                {
                    text += $"\n{description}";
                }
            }

            return new ReadingTextItem.MastodonSensitiveFavoriteNotificationReadingTextItem(AccountIdentifier, text);
        }
    }
}
