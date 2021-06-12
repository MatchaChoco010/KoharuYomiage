using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.AddMisskeyAccount
{
    public interface IAddMisskeyAccount
    {
        Task Login(string instance, CancellationToken cancellationToken);
    }
}
