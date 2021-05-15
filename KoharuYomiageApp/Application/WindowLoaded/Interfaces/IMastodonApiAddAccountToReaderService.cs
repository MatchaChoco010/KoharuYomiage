namespace KoharuYomiageApp.Application.WindowLoaded.Interfaces
{
    public interface IMastodonApiAddAccountToReaderService
    {
        void AddAccountToReader(string accountIdentifier, string instance, string accessToken);
    }
}
