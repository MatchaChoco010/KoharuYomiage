namespace MastodonApi.Payloads.Entities
{
    public record Relationship(string id, bool following, bool requested, bool endorsed, bool folloewd_by, bool muting,
        bool muting_notifications, bool showing_reblogs, bool notifying, bool blocking, bool domain_blocking,
        bool blocked_by, string note);
}
