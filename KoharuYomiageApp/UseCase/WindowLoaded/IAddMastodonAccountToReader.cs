using KoharuYomiageApp.UseCase.WindowLoaded.DataObjects;

namespace KoharuYomiageApp.UseCase.WindowLoaded
{
    public interface IAddMastodonAccountToReader
    {
        void AddMastodonAccountToReader(AddReaderInfo account);
    }
}
