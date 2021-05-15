using System.Collections.Generic;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.Interfaces.DataObjects;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public interface IMastodonAccountStorage
    {
        Task<MastodonAccountSaveData?> FindMastodonAccountData(string identifier);
        Task SaveMastodonAccountData(MastodonAccountSaveData accountSaveData);
        Task<IEnumerable<MastodonAccountSaveData>> GetMastodonAccountData();
    }
}
