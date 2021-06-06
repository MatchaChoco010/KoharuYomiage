using System.Collections.Generic;

namespace KoharuYomiageApp.Presentation.Mastodon.DataObjects
{
    public record MastodonSensitiveReblogNotificationData(string Username, string Instance,
        string ReblogUserDisplayName, string ReblogUserUsername, string SpoilerText, string Content,
        IEnumerable<string>? MediaDescriptions);
}
