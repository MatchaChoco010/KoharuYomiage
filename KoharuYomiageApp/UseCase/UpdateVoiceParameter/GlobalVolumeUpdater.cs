using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.UpdateVoiceParameter
{
    public class GlobalVolumeUpdater : IUpdateGlobalVolume
    {
        readonly IGlobalVolumeRepository _globalVolumeRepository;

        public GlobalVolumeUpdater(IGlobalVolumeRepository globalVolumeRepository)
        {
            _globalVolumeRepository = globalVolumeRepository;
        }

        public async Task Update(double volume, CancellationToken cancellationToken)
        {
            var globalVolume = await _globalVolumeRepository.GetGlobalVolume(cancellationToken);
            globalVolume.Volume.Value = volume;
            await _globalVolumeRepository.SaveGlobalVolume(globalVolume, cancellationToken);
        }
    }
}
