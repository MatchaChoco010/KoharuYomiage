using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public interface IMastodonClientStorage
    {
        ValueTask<MastodonClientData?> FindMastodonClientData(string instance);
        ValueTask SaveMastodonClientData(MastodonClientData clientData);
    }
}
