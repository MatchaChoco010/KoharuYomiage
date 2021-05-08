using System;

namespace MastodonApi.Payloads.Entities
{
    public record IdentityProof(string provider, string provider_username, string profile_url, string proof_url,
        DateTime updated_at);
}
