using System.Collections.Generic;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces.DataObjects
{
    public record MastodonSensitiveStatusInputData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername, string SpoilerText, string Content, IEnumerable<string>? MediaDescriptions);
}
