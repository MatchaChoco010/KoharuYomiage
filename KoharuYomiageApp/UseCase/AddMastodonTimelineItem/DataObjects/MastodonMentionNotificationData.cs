using System.Collections.Generic;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects
{
    public record MastodonMentionNotificationData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername, string Content, IEnumerable<string>? MediaDescriptions);
}
