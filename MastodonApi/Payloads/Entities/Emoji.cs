namespace MastodonApi.Payloads.Entities
{
    public record Emoji(string shortcode, string url, string static_url, bool visible_in_picker, string? category);
}
