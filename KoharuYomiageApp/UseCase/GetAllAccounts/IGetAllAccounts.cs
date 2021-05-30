using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.GetAllAccounts.DataObjects;

namespace KoharuYomiageApp.UseCase.GetAllAccounts
{
    public interface IGetAllAccounts
    {
        Task<IEnumerable<AccountData>> GetAllAccounts(CancellationToken cancellationToken);
    }
}
