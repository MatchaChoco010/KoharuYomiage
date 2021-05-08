using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MastodonApi.Payloads.Entities
{
    public record ScheduledStatus(string id, string scheduled_at,
        [property: JsonExtensionData] Dictionary<string, object> _params,
        IReadOnlyList<Attachment> media_attachments);
}
