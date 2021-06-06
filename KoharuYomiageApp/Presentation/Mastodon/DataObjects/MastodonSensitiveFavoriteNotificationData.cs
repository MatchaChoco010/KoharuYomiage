using System.Collections.Generic;

namespace KoharuYomiageApp.Presentation.Mastodon.DataObjects
{
    public record MastodonSensitiveFavoriteNotificationData(string Username, string Instance,
        string FavoriteUserDisplayName, string FavoriteUsername, string SpoilerText, string Content,
        IEnumerable<string>? MediaDescriptions);
}
