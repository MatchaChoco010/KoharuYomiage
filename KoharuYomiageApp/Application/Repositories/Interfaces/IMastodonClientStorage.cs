using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.Interfaces.DataObjects;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public interface IMastodonClientStorage
    {
        Task<MastodonClientSaveData?> FindMastodonClientData(string instance);
        Task SaveMastodonClientData(MastodonClientSaveData clientSaveData);
    }
}
