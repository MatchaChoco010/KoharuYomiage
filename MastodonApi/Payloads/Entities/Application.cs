namespace MastodonApi.Payloads.Entities
{
    public record Application(string name, string website, string vapid_key, string client_id, string client_secret);
}
