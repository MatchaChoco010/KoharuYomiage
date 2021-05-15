using System.Collections.Generic;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects
{
    public record MastodonBoostedSensitiveStatusData(string Username, string Instance, string BoostedUserDisplayName,
        string BoostedUserUserName, string AuthorDisplayName, string AuthorUsername, string SpoilerText, string Content,
        bool Muted, IEnumerable<string>? MediaDescriptions);
}
