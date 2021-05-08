using System;
using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Filter(string id, string phrase, IReadOnlyCollection<string> context, DateTime expires_at,
        bool irreversible, bool whole_word);
}
