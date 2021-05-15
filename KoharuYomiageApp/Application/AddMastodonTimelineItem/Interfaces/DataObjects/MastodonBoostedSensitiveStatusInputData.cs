using System.Collections.Generic;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces.DataObjects
{
    public record MastodonBoostedSensitiveStatusInputData(string Username, string Instance,
        string BoostedUserDisplayName, string BoostedUserUserName, string AuthorDisplayName, string AuthorUsername,
        string SpoilerText, string Content, bool Muted, IEnumerable<string>? MediaDescriptions);
}
