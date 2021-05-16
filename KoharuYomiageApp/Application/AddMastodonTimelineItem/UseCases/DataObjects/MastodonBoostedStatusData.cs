using System.Collections.Generic;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects
{
    public record MastodonBoostedStatusData(string Username, string Instance, string BoostedUserDisplayName,
        string BoostedUserUserName, string AuthorDisplayName, string AuthorUsername, string Content,
        IEnumerable<string>? MediaDescriptions);
}
