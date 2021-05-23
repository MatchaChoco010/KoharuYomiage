using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository.DataObjects;

namespace KoharuYomiageApp.Data.Repository
{
    public interface IMastodonClientStorage
    {
        Task<MastodonClientData?> FindMastodonClientData(string instance, CancellationToken cancellationToken);
        Task SaveMastodonClientData(MastodonClientData clientSaveData, CancellationToken cancellationToken);
    }
}
