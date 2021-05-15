namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public interface IMastodonApiAddAccountToReaderService
    {
        void AddAccountToReader(string accountIdentifier, string instance, string accessToken);
    }
}
