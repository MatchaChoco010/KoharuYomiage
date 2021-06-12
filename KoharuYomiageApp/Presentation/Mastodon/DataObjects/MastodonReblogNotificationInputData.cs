using System.Collections.Generic;

namespace KoharuYomiageApp.Presentation.Mastodon.DataObjects
{
    public record MastodonReblogNotificationInputData(string Username, string Instance, string ReblogUserDisplayName,
        string ReblogUserUsername, string Content, IEnumerable<string>? MediaDescriptions);
}
