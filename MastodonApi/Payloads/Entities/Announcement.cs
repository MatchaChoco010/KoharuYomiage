using System;
using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Announcement(string id, string text, bool published, bool all_day, DateTime created_at,
        DateTime updated_at, bool read, IReadOnlyCollection<AnnouncementReaction> reactions, DateTime scheduled_at,
        DateTime starts_at, DateTime ends_at);
}
