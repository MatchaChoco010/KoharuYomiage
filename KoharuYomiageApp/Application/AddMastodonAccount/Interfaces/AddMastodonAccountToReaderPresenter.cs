using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public class AddMastodonAccountToReaderPresenter : IAddMastodonAccountToReader
    {
        readonly IMastodonApiAddAccountToReaderService _addAccountToReaderService;

        public AddMastodonAccountToReaderPresenter(IMastodonApiAddAccountToReaderService addAccountToReaderService)
        {
            _addAccountToReaderService = addAccountToReaderService;
        }

        public void AddMastodonAccountToReader(AddReaderInfo addReaderInfo)
        {
            _addAccountToReaderService.AddAccountToReader(addReaderInfo.AccountIdentifier, addReaderInfo.Username,
                addReaderInfo.Instance,
                addReaderInfo.AccessToken);
        }
    }
}
