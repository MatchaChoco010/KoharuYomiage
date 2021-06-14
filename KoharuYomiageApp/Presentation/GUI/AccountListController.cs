using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account.Mastodon;
using KoharuYomiageApp.UseCase.DeleteAccount;
using KoharuYomiageApp.UseCase.GetAllAccounts;
using KoharuYomiageApp.UseCase.SwitchAccountConnection;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class AccountListController
    {
        readonly IGetAllAccounts _getAllAccounts;
        readonly ISwitchAccountConnection _switchAccountConnection;
        readonly IDeleteAccount _deleteAccount;

        public AccountListController(IGetAllAccounts getAllAccounts, ISwitchAccountConnection switchAccountConnection,
            IDeleteAccount deleteAccount)
        {
            _getAllAccounts = getAllAccounts;
            _switchAccountConnection = switchAccountConnection;
            _deleteAccount = deleteAccount;
        }

        public async Task<IEnumerable<(string, string, string, Uri, bool, string)>> GetAllAcounts(
            CancellationToken cancellationToken)
        {
            var data = await _getAllAccounts.GetAllAccounts(cancellationToken);
            return data.Select(d => (d.Id, d.Username, d.Instance, d.IconUrl, d.IsReadingPostFromThisAccount,
                    d.Type));
        }

        public async Task SwitchConnection(string username, string instance, bool connect,
            CancellationToken cancellationToken)
        {
            await _switchAccountConnection.SwitchAccountConnection(username, instance, connect, cancellationToken);
        }

        public async Task DeleteAccount(string username, string instance, CancellationToken cancellationToken)
        {
            await _deleteAccount.DeleteAccount(username, instance, cancellationToken);
        }
    }
}
