using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.AddMisskeyAccount
{
    public interface IRegisterClient
    {
        Task<string> RegisterClient(string instance, CancellationToken cancellationToken);
    }
}
