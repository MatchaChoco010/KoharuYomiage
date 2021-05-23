using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.VoiceParameters;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IGlobalVolumeRepository
    {
        Task<GlobalVolume> GetGlobalVolume(CancellationToken cancellationToken);
        Task SaveGlobalVolume(GlobalVolume volume, CancellationToken cancellationToken);
    }
}
