using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.Interfaces.DataObjects;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Application.Repositories.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public class MastodonClientGateway : IMastodonClientGateway
    {
        readonly IMastodonClientStorage _storage;

        public MastodonClientGateway(IMastodonClientStorage storage)
        {
            _storage = storage;
        }

        public async Task<MastodonClientData?> FindMastodonClientData(string instance)
        {
            var data = await _storage.FindMastodonClientData(instance);
            return data is not null ? new MastodonClientData(data.Instance, data.Id, data.Secret) : null;
        }

        public async Task SaveMastodonClientData(MastodonClientData clientData)
        {
            await _storage.SaveMastodonClientData(new MastodonClientSaveData(clientData.Instance, clientData.Id,
                clientData.Secret));
        }
    }
}
