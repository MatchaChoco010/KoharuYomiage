using System.Collections.Generic;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects
{
    public record MastodonStatusData(string Username, string Instance, string AuthorDisplayName, string AuthorUsername,
        string Content, IEnumerable<string>? MediaDescriptions);
}
