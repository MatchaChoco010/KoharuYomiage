using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public interface IMastodonClientGateway
    {
        Task<MastodonClientData?> FindMastodonClientData(string instance);
        Task SaveMastodonClientData(MastodonClientData clientData);
    }
}
