using System.Collections.Generic;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public interface IMastodonAccountGateway
    {
        Task<MastodonAccountData?> FindMastodonAccountData(string identifier);
        Task SaveMastodonAccountData(MastodonAccountData accountData);
        Task<IEnumerable<MastodonAccountData>> GetMastodonAccountData();
    }
}
