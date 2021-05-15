using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public interface IMastodonClientStorage
    {
        Task<MastodonClientData?> FindMastodonClientData(string instance);
        Task SaveMastodonClientData(MastodonClientData clientData);
    }
}
