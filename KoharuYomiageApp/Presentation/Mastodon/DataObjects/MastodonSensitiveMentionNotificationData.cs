﻿using System.Collections.Generic;

namespace KoharuYomiageApp.Presentation.Mastodon.DataObjects
{
    public record MastodonSensitiveMentionNotificationData(string Username, string Instance, string AuthorDisplayName,
        string AuthorUsername, string SpoilerText, string Content, IEnumerable<string>? MediaDescriptions);
}
