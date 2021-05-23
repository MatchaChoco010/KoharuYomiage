namespace KoharuYomiageApp.Presentation.Mastodon
{
    public interface IMastodonAddAccountToReader
    {
        void AddAccountToReader(string accountIdentifier, string username, string instance, string accessToken);
    }
}
