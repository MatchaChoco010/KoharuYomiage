using System;
using System.Collections.Generic;

namespace MisskeyApi.Payloads.Entities
{
    public record Note(string id, DateTime createdAt, string? text, string? cw, User user, string userId, Note? reply,
        string? replyId, Note? renote, string? renoteId, List<DriveFile> files, List<string> fileIds, string visibility,
        string? myReaction, Dictionary<string, int> reactions, Poll? poll);

    public record Poll(DateTime? expiresAt, bool multiple, List<Choice> choices);

    public record Choice(bool isVoted, string text, int votes);
}
