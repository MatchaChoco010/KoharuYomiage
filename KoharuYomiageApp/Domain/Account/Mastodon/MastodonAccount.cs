namespace KoharuYomiageApp.Domain.Account.Mastodon
{
    public record MastodonAccount : Account
    {
        public MastodonAccount(Username username, Instance instance, DisplayName displayName,
            MastodonAccessToken accessToken, MastodonAccountIconUrl iconUrl,
            IsReadingPostsFromThisAccount isReadingPostsFromThisAccount)
            : base(username, instance, displayName, isReadingPostsFromThisAccount)
        {
            AccessToken = accessToken;
            IconUrl = iconUrl;
        }

        public MastodonAccessToken AccessToken { get; init; }

        public MastodonAccountIconUrl IconUrl { get; init; }
    }
}
