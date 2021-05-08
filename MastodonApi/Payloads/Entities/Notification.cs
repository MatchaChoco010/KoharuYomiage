using System;

namespace MastodonApi.Payloads.Entities
{
    public record Notification(string id, string type, DateTime created_at, Account account, Status? status);
}
