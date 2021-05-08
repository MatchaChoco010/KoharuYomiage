using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record PushSubscription(string id, string endpoint, string server_key, Dictionary<string, object> alerts);
}
