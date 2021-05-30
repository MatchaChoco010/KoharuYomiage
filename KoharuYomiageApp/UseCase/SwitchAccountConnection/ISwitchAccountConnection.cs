using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.SwitchAccountConnection
{
    public interface ISwitchAccountConnection
    {
        Task SwitchAccountConnection(string username, string instance, bool connect,
            CancellationToken cancellationToken);
    }
}
