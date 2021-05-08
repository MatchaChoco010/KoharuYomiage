namespace MastodonApi.Payloads.Entities
{
    public record Card(string url, string title, string description, string type, string? author_name,
        string? author_url, string? provider_name, string? provider_url, string? html, float? width, float? height,
        string? image, string? embed_url, string? blurhash);
}
