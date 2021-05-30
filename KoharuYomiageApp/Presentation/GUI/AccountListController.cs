using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.GetAllAccounts;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class AccountListController
    {
        IGetAllAccounts _getAllAccounts;

        public AccountListController(IGetAllAccounts getAllAccounts)
        {
            _getAllAccounts = getAllAccounts;
        }

        public async Task<IEnumerable<(string, string, string, Uri)>> GetAllAcounts(CancellationToken cancellationToken)
        {
            var data = await _getAllAccounts.GetAllAccounts(cancellationToken);
            return data.Select(d => (d.Id, d.Username, d.Instance, d.IconUrl));
        }
    }
}
