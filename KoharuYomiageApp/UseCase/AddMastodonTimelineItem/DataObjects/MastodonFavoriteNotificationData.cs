using System.Collections.Generic;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects
{
    public record MastodonFavoriteNotificationData(string Username, string Instance, string FavoriteUserDisplayName,
        string FavoriteUsername, string Content, IEnumerable<string>? MediaDescriptions);
}
