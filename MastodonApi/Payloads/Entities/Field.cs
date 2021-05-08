using System;

namespace MastodonApi.Payloads.Entities
{
    public record Field(string name, string value, DateTime? verified_at);
}
