namespace KoharuYomiageApp.Entities.Account.Mastodon
{
    public record MastodonAccount : Account
    {
        public MastodonAccount(Username username, Instance instance, MastodonAccessToken accessToken,
            MastodonAccountIconUrl iconUrl)
            : base(username, instance)
        {
            AccessToken = accessToken;
            IconUrl = iconUrl;
        }

        public MastodonAccessToken AccessToken { get; init; }

        public MastodonAccountIconUrl IconUrl { get; init; }
    }
}
