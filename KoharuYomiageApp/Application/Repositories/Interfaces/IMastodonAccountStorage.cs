using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public interface IMastodonAccountStorage
    {
        ValueTask<MastodonAccountData?> FindMastodonAccountData(string identifier);
        ValueTask SaveMastodonAccountData(MastodonAccountData accountData);
    }
}
