using System.Collections.Generic;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects
{
    public record MastodonSensitiveMentionNotificationData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername, string SpoilerText, string Content, IEnumerable<string>? MediaDescriptions);
}
