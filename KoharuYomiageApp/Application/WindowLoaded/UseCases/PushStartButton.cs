using System.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Application.WindowLoaded.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.WindowLoaded.UseCases
{
    public class AccountExistenceChecker : IPushStartButton
    {
        readonly IAddMastodonAccountToReader _addMastodonAccountToReader;
        readonly MastodonAccountRepository _mastodonAccountRepository;
        readonly IStartApp _startApp;
        readonly IStartRegisteringAccount _startRegisteringAccount;

        public AccountExistenceChecker(MastodonAccountRepository mastodonAccountRepository,
            IStartRegisteringAccount startRegisteringAccount, IAddMastodonAccountToReader addMastodonAccountToReader,
            IStartApp startApp)
        {
            _mastodonAccountRepository = mastodonAccountRepository;
            _startRegisteringAccount = startRegisteringAccount;
            _addMastodonAccountToReader = addMastodonAccountToReader;
            _startApp = startApp;
        }

        public async Task PushStartButton()
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
                    _addMastodonAccountToReader.AddMastodonAccountToReader(
                        new AddReaderInfo(account.AccountIdentifier.Value, account.Username.Value,
                            account.Instance.Value, account.AccessToken.Token));
                }

                _startApp.StartApp();
            }
        }
    }
}
