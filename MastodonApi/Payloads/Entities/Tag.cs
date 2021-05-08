using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Tag(string name, string url, IReadOnlyList<History>? history);
}
