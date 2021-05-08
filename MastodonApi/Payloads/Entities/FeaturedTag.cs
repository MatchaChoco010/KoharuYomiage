using System;

namespace MastodonApi.Payloads.Entities
{
    public record FeaturedTag(string id, string name, string url, uint statuses_count, DateTime last_status_at);
}
