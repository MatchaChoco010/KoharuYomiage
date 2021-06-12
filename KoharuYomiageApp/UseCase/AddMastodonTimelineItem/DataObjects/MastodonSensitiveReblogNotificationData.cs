using System.Collections.Generic;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects
{
    public record MastodonSensitiveReblogNotificationData(string Username, string Instance,
        string ReblogUserDisplayName, string ReblogUserUsername, string SpoilerText, string Content,
        IEnumerable<string>? MediaDescriptions);
}
