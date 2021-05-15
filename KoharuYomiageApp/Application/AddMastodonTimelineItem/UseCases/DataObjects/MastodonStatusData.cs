using System.Collections.Generic;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects
{
    public record MastodonStatusData(string Username, string Instance, string AuthorDisplayName, string AuthorUsername,
        string Content, bool Muted, IEnumerable<string>? MediaDescriptions);
}
