namespace MisskeyApi.Payloads
{
    public abstract record UserStreamPayload
    {
        public record Note(Entities.Note Item) : UserStreamPayload;
        public record Notification(Entities.Notification Item) : UserStreamPayload;
    }
}
