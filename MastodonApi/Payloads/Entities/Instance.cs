using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Instance(string uri, string title, string description, string short_description, string email,
        string version, IReadOnlyList<string> languages, bool registrations, bool approval_required,
        bool invites_enabled, Dictionary<string, object> urls, Dictionary<string, object> stats, uint user_count,
        uint status_count, uint domain_count, string? thumbnail, Account? contact_account);
}
