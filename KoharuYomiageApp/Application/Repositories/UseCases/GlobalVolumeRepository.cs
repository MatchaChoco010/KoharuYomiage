using System.Threading.Tasks;
using KoharuYomiageApp.Entities.VoiceParameters;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public class GlobalVolumeRepository
    {
        readonly IGlobalVolumeGateway _gateway;

        GlobalVolume? _globalVolume;

        public GlobalVolumeRepository(IGlobalVolumeGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<GlobalVolume> GetGlobalVolume()
        {
            if (_globalVolume is not null)
            {
                return _globalVolume;
            }

            var volume = await _gateway.FindGlobalVolume();
            if (volume is not null)
            {
                _globalVolume = new GlobalVolume(volume.Value);
                return _globalVolume;
            }

            _globalVolume = new GlobalVolume();
            return _globalVolume;
        }

        public async Task SaveGlobalVolume(GlobalVolume volume)
        {
            await _gateway.SaveGlobalVolume(volume.Volume.Value);
        }
    }
}
