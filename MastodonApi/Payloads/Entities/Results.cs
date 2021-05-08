using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Results(IReadOnlyList<Account> accounts, IReadOnlyList<Status> statuses,
        IReadOnlyList<string> hashtags);
}
