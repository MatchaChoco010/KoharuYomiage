using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository.DataObjects;

namespace KoharuYomiageApp.Data.Repository
{
    public interface IMisskeyAccountStorage
    {
        Task<MisskeyAccountData?> FindMisskeyAccountData(string identifier, CancellationToken cancellationToken);
        Task SaveMisskeyAccountData(MisskeyAccountData accountSaveData, CancellationToken cancellationToken);
        Task DeleteMisskeyAccountData(string identifier, CancellationToken cancellationToken);
        Task<IEnumerable<MisskeyAccountData>> GetMisskeyAccountData(CancellationToken cancellationToken);
    }
}
