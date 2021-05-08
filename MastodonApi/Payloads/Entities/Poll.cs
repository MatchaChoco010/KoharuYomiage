using System;
using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Poll(string id, DateTime expires_at, bool expired, bool multiple, uint votes_count,
        uint? voters_count, bool? voted, IReadOnlyList<uint>? own_votes,
        IReadOnlyList<Dictionary<string, object>> options, IReadOnlyList<Emoji> emojis);
}
