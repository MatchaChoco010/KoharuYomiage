using System.Collections.Generic;

namespace KoharuYomiageApp.Presentation.Mastodon.DataObjects
{
    public record MastodonFavoriteNotificationData(string Username, string Instance, string FavoriteUserDisplayName,
        string FavoriteUsername, string Content, IEnumerable<string>? MediaDescriptions);
}
