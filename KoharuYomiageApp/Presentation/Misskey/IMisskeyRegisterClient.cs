using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Presentation.Misskey
{
    public interface IMisskeyRegisterClient
    {
        Task<string> RegisterClient(string instance, CancellationToken cancellationToken = new());
    }
}
