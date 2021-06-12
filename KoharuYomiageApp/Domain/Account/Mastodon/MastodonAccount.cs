namespace KoharuYomiageApp.Domain.Account.Mastodon
{
    public record MastodonAccount : Account
    {
        public MastodonAccount(Username username, Instance instance, DisplayName displayName,
            MastodonAccessToken accessToken, AccountIconUrl iconUrl,
            IsReadingPostsFromThisAccount isReadingPostsFromThisAccount)
            : base(username, instance, displayName, iconUrl, isReadingPostsFromThisAccount)
        {
            AccessToken = accessToken;
        }

        public MastodonAccessToken AccessToken { get; init; }
    }
}
