namespace MastodonApi.Payloads.Entities
{
    public record Token(string access_token, string token_type, string scope, uint created_at);
}
