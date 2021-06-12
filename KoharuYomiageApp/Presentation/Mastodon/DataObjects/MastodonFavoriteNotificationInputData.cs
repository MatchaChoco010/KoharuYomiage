using System.Collections.Generic;

namespace KoharuYomiageApp.Presentation.Mastodon.DataObjects
{
    public record MastodonFavoriteNotificationInputData(string Username, string Instance, string FavoriteUserDisplayName,
        string FavoriteUsername, string Content, IEnumerable<string>? MediaDescriptions);
}
