using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Context(IReadOnlyCollection<Status> ancestors, IReadOnlyCollection<Status> descendants);
}
