namespace MastodonApi.Payloads.Entities
{
    public record Conversation(string id, Account account, bool unread, Status? last_status);
}
