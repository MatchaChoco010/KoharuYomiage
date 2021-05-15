using KoharuYomiageApp.Application.WindowLoaded.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.WindowLoaded.UseCases
{
    public interface IAddMastodonAccountToReader
    {
        void AddMastodonAccountToReader(AddReaderInfo account);
    }
}
