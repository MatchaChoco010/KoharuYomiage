using System.Collections.Generic;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces.DataObjects
{
    public record MastodonBoostedStatusInputData(string Username, string Instance, string BoostedUserDisplayName,
        string BoostedUserUserName, string AuthorDisplayName, string AuthorUsername, string Content, bool Muted,
        IEnumerable<string>? MediaDescriptions);
}
