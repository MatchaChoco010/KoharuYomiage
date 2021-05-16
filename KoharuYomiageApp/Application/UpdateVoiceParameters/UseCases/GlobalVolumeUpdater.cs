using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases;

namespace KoharuYomiageApp.Application.UpdateVoiceParameters.UseCases
{
    public class GlobalVolumeUpdater : IUpdateGlobalVolume
    {
        readonly GlobalVolumeRepository _globalVolumeRepository;

        public GlobalVolumeUpdater(GlobalVolumeRepository globalVolumeRepository)
        {
            _globalVolumeRepository = globalVolumeRepository;
        }

        public async Task Update(double volume)
        {
            var globalVolume = await _globalVolumeRepository.GetGlobalVolume();
            globalVolume.Volume.Value = volume;
            await _globalVolumeRepository.SaveGlobalVolume(globalVolume);
        }
    }
}
