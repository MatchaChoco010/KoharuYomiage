using System.Threading.Tasks;
using KoharuYomiageApp.Entities.VoiceParameters;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public class GlobalVolumeRepository
    {
        readonly IGlobalVolumeGateway _gateway;

        public GlobalVolumeRepository(IGlobalVolumeGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<GlobalVolume> GetGlobalVolume()
        {
            var volume = await _gateway.FindGlobalVolume();
            if (volume is not null)
            {
                return new GlobalVolume(volume.Value);
            }

            var globalVolume = new GlobalVolume();
            await _gateway.SaveGlobalVolume(globalVolume.Volume);
            return globalVolume;
        }

        public async Task SaveGlobalVolume(GlobalVolume volume)
        {
            await _gateway.SaveGlobalVolume(volume.Volume);
        }
    }
}
