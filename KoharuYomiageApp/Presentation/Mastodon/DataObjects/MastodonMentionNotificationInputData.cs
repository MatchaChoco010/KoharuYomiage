using System.Collections.Generic;

namespace KoharuYomiageApp.Presentation.Mastodon.DataObjects
{
    public record MastodonMentionNotificationInputData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername, string Content, IEnumerable<string>? MediaDescriptions);
}
