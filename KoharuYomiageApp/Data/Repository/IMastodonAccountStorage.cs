using System.Collections.Generic;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository.DataObjects;

namespace KoharuYomiageApp.Data.Repository
{
    public interface IMastodonAccountStorage
    {
        Task<MastodonAccountData?> FindMastodonAccountData(string identifier);
        Task SaveMastodonAccountData(MastodonAccountData accountSaveData);
        Task<IEnumerable<MastodonAccountData>> GetMastodonAccountData();
    }
}
