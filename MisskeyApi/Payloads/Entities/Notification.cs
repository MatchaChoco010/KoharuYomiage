using System;

namespace MisskeyApi.Payloads.Entities
{
    public abstract record Notification
    {
        public record Reaction(string id, DateTime createdAt, bool isRead, string reaction, User user, string userId, Note note) : Notification;
        public record Reply(string id, DateTime createdAt, bool isRead, User user, string userId, Note note) : Notification;
        public record Renote(string id, DateTime createdAt, bool isRead, User user, string userId, Note note) : Notification;
        public record Quote(string id, DateTime createdAt, bool isRead, User user, string userId, Note note) : Notification;
        public record Mention(string id, DateTime createdAt, bool isRead, User user, string userId, Note note) : Notification;
        public record PollVote(string id, DateTime createdAt, bool isRead, User user, string userId, Note note) : Notification;
        public record Follow(string id, DateTime createdAt, bool isRead, User user, string userId) : Notification;
        public record FollowRequestAccepted(string id, DateTime createdAt, bool isRead, User user, string userId) : Notification;
        public record ReceiveFollowRequest(string id, DateTime createdAt, bool isRead, User user, string userId) : Notification;
        public record GroupInvited(string id, DateTime createdAt, bool isRead) : Notification;
        public record App(string id, DateTime createdAt, bool isRead, string body, string icon) : Notification;
    }
}
