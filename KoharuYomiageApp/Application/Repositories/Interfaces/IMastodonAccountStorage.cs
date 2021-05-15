using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public interface IMastodonAccountStorage
    {
        Task<MastodonAccountData?> FindMastodonAccountData(string identifier);
        Task SaveMastodonAccountData(MastodonAccountData accountData);
        Task<IEnumerable<MastodonAccountData>> GetMastodonAccountData();
    }
}
