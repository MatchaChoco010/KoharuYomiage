using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Context(List<Status> ancestors, List<Status> descendants);
}
