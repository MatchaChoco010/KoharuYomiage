using System.Collections.Generic;
using KoharuYomiageApp.Application.WindowLoaded.UseCases;
using KoharuYomiageApp.Application.WindowLoaded.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.WindowLoaded.Interfaces
{
    public class AddMastodonAccountToReaderPresenter : IAddMastodonAccountToReader
    {
        readonly IMastodonApiAddAccountToReaderService _addAccountToReaderService;

        public AddMastodonAccountToReaderPresenter(IMastodonApiAddAccountToReaderService addAccountToReaderService)
        {
            _addAccountToReaderService = addAccountToReaderService;
        }

        public void AddMastodonAccountToReader(AddReaderInfo info)
        {
            _addAccountToReaderService.AddAccountToReader(info.AccountIdentifier, info.Instance, info.AccessToken);
        }
    }
}
