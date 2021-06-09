using System;
using System.Collections.Generic;

namespace MisskeyApi.Payloads.Entities
{
    public record DriveFile(string id, DateTime createdAt, bool isSensitive, string name, Uri thumbnailUrl, Uri url,
        string type, int size, string md5, string blurhash, Dictionary<string, dynamic> properties);
}
