using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Attachment(string id, string type, string url, string preview_url, string? remote_url,
        string? text_url, Dictionary<string, object>? meta, string? description, string? blurhash);
}
