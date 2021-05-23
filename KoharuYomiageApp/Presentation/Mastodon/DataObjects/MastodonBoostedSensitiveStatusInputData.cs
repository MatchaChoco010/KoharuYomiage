using System.Collections.Generic;

namespace KoharuYomiageApp.Presentation.Mastodon.DataObjects
{
    public record MastodonBoostedSensitiveStatusInputData(string Username, string Instance,
        string BoostedUserDisplayName, string BoostedUserUserName, string AuthorDisplayName, string AuthorUsername,
        string SpoilerText, string Content, IEnumerable<string>? MediaDescriptions);
}
