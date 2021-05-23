using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.UpdateVoiceParameter
{
    public interface IStartUpdatingVoiceParameter
    {
        Task Start(CancellationToken cancellationToken);
    }
}
