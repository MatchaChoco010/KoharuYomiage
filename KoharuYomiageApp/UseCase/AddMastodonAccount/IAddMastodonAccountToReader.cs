using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonAccount
{
    public interface IAddMastodonAccountToReader
    {
        void AddMastodonAccountToReader(AddReaderInfo addReaderInfo);
    }
}
