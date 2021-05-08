using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Source(string note, IReadOnlyList<Field> fields, string? privacy, bool? sensitive, string? language,
        uint? follow_requests_count);
}
