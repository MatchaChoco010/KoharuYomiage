using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.GetGlobalVolume
{
    public class GlobalVolumeProvider : IGetGlobalVolume
    {
        readonly IGlobalVolumeRepository _globalVolumeRepository;

        public GlobalVolumeProvider(IGlobalVolumeRepository globalVolumeRepository)
        {
            _globalVolumeRepository = globalVolumeRepository;
        }

        public async Task<double> GetGlobalVolume(CancellationToken cancellationToken)
        {
            var globalVolume = await _globalVolumeRepository.GetGlobalVolume(cancellationToken);
            return globalVolume.Volume.Value;
        }
    }
}
