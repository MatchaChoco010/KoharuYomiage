namespace MastodonApi.Payloads
{
    public abstract record UserStreamPayload
    {
        public record Notification(Entities.Notification Item) : UserStreamPayload;

        public record Status(Entities.Status Item) : UserStreamPayload;
    }
}
