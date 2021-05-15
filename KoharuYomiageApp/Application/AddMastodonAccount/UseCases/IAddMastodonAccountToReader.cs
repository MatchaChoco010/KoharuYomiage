using KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonAccount.UseCases
{
    public interface IAddMastodonAccountToReader
    {
        void AddMastodonAccountToReader(AddReaderInfo addReaderInfo);
    }
}
