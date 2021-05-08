using System;
using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Account(string id, string username, string acct, string url, string display_name, string note,
        string avatar, string avatar_static, string header, string header_static, bool locked,
        IReadOnlyCollection<Emoji> emojis, bool discoverable, DateTime created_at, DateTime last_status_at,
        uint statuses_count, uint followers_count, uint following_count, Account? moved,
        IReadOnlyCollection<Field> fields, bool? bot, Source? source, bool? suspended, DateTime? mute_expires_at);
}
