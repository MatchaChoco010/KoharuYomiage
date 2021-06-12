namespace KoharuYomiageApp.Domain.Account.Misskey
{
    public record MisskeyAccount : Account
    {
        public MisskeyAccount(Username username, Instance instance, DisplayName displayName,
            MisskeyAccessToken accessToken, AccountIconUrl iconUrl,
            IsReadingPostsFromThisAccount isReadingPostsFromThisAccount)
            : base(username, instance, displayName, iconUrl, isReadingPostsFromThisAccount)
        {
            AccessToken = accessToken;
        }

        public MisskeyAccessToken AccessToken { get; init; }
    }
}
