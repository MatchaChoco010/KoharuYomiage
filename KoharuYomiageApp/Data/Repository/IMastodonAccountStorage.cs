using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository.DataObjects;

namespace KoharuYomiageApp.Data.Repository
{
    public interface IMastodonAccountStorage
    {
        Task<MastodonAccountData?> FindMastodonAccountData(string identifier, CancellationToken cancellationToken);
        Task SaveMastodonAccountData(MastodonAccountData accountSaveData, CancellationToken cancellationToken);
        Task DeleteMastodonAccountData(string identifier, CancellationToken cancellationToken);
        Task<IEnumerable<MastodonAccountData>> GetMastodonAccountData(CancellationToken cancellationToken);
    }
}
