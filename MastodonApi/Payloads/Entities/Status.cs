using System;
using System.Collections.Generic;

namespace MastodonApi.Payloads.Entities
{
    public record Status(string id, string uri, DateTime created_at, Account account, string content, string visibility,
        bool sensitive, string spoiler_text, IReadOnlyList<Attachment> media_attachments, Application application,
        IReadOnlyList<Mention> mentions, IReadOnlyList<Tag> tags, IReadOnlyList<Emoji> emojis, uint rebloogs_count,
        uint favourites_count, uint replies_count, string? url, string? in_reply_to_id, string? in_reply_to_account_id,
        Status? reblog, Poll? poll, Card? card, string? language, string? text, bool favourited, bool reblogged,
        bool muted, bool bookmarked, bool pinned);
}
