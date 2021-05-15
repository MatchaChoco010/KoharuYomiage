using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.Interfaces.DataObjects;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Application.Repositories.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public class MastodonAccountGateway : IMastodonAccountGateway
    {
        readonly IMastodonAccountStorage _storage;

        public MastodonAccountGateway(IMastodonAccountStorage storage)
        {
            _storage = storage;
        }

        public async Task<MastodonAccountData?> FindMastodonAccountData(string identifier)
        {
            var data = await _storage.FindMastodonAccountData(identifier);
            return data is not null
                ? new MastodonAccountData(data.Username, data.Instance, data.AccessToken, data.IconUrl)
                : null;
        }

        public async Task SaveMastodonAccountData(MastodonAccountData accountData)
        {
            await _storage.SaveMastodonAccountData(new MastodonAccountSaveData(accountData.Username,
                accountData.Instance, accountData.AccessToken, accountData.IconUrl));
        }

        public async Task<IEnumerable<MastodonAccountData>> GetMastodonAccountData()
        {
            var data = await _storage.GetMastodonAccountData();
            return data.Select(d => new MastodonAccountData(d.Username, d.Instance, d.AccessToken, d.IconUrl));
        }
    }
}
