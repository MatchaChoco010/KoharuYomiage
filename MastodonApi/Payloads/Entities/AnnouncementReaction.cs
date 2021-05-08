namespace MastodonApi.Payloads.Entities
{
    public record AnnouncementReaction(string name, uint count, bool me, string url, string static_url);
}
