using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.DeleteAccount
{
    public interface IDeleteAccount
    {
        Task DeleteAccount(string username, string instance, CancellationToken cancellationToken);
    }
}
