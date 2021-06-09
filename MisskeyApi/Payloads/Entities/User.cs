using System;
using System.Collections.Generic;

namespace MisskeyApi.Payloads.Entities
{
    public record User(string id, string username, string host, string name, string onlineStatus, Uri avatarUrl,
        string avatarBlurhash, List<Emoji> emojis);
}
