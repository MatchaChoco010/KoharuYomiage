using System.Collections.Generic;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects
{
    public record MastodonSensitiveStatusData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername, string SpoilerText, string Content, IEnumerable<string>? MediaDescriptions);
}
