using System.Collections.Generic;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces.DataObjects
{
    public record MastodonStatusInputData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername,
        string Content, bool Muted, IEnumerable<string>? MediaDescriptions);
}
