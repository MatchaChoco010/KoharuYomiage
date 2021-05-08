using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Marker(Dictionary<string, object> home, Dictionary<string, object> notifications);
}
