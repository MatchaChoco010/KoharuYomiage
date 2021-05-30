using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.GetAllAccounts;
using KoharuYomiageApp.UseCase.SwitchAccountConnection;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class AccountListController
    {
        readonly IGetAllAccounts _getAllAccounts;
        readonly ISwitchAccountConnection _switchAccountConnection;

        public AccountListController(IGetAllAccounts getAllAccounts, ISwitchAccountConnection switchAccountConnection)
        {
            _getAllAccounts = getAllAccounts;
            _switchAccountConnection = switchAccountConnection;
        }

        public async Task<IEnumerable<(string, string, string, Uri, bool)>> GetAllAcounts(
            CancellationToken cancellationToken)
        {
            var data = await _getAllAccounts.GetAllAccounts(cancellationToken);
            return data.Select(d => (d.Id, d.Username, d.Instance, d.IconUrl, d.IsReadingPostFromThisAccount));
        }

        public async Task SwitchConnection(string username, string instance, bool connect,
            CancellationToken cancellationToken)
        {
            await _switchAccountConnection.SwitchAccountConnection(username, instance, connect, cancellationToken);
        }
    }
}
