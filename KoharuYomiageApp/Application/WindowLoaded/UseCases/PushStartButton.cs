using System.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Application.WindowLoaded.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.WindowLoaded.UseCases
{
    public class AccountExistenceChecker : IPushStartButton
    {
        readonly IMastodonAccountRepository _mastodonAccountRepository;
        readonly IStartRegisteringAccount _startRegisteringAccount;
        readonly IAddMastodonAccountToReader _addMastodonAccountToReader;
        readonly IStartApp _startApp;

        public AccountExistenceChecker(IMastodonAccountRepository mastodonAccountRepository, IStartRegisteringAccount startRegisteringAccount, IAddMastodonAccountToReader addMastodonAccountToReader, IStartApp startApp)
        {
            _mastodonAccountRepository = mastodonAccountRepository;
            _startRegisteringAccount = startRegisteringAccount;
            _addMastodonAccountToReader = addMastodonAccountToReader;
            _startApp = startApp;
        }

        public async ValueTask PushStartButton()
        {
            var mastodonAccounts = (await _mastodonAccountRepository.GetMastodonAccounts()).ToArray();

            if (mastodonAccounts.Length is 0)
            {
                _startRegisteringAccount.StartRegisteringAccount();
            }
            else
            {
                foreach (var account in mastodonAccounts)
                {
                    _addMastodonAccountToReader.AddMastodonAccountToReader(new AddReaderInfo(account.AccountIdentifier.Value,  account.Instance.Value, account.AccessToken.Token));
                }
                _startApp.StartApp();
            }
        }
    }
}
