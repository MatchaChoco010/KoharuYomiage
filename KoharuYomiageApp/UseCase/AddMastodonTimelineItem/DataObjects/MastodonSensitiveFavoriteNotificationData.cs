using System.Collections.Generic;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects
{
    public record MastodonSensitiveFavoriteNotificationData(string Username, string Instance,
        string FavoriteUserDisplayName, string FavoriteUsername, string SpoilerText, string Content,
        IEnumerable<string>? MediaDescriptions);
}
