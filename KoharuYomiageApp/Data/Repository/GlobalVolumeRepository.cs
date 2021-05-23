using System.Threading.Tasks;
using KoharuYomiageApp.Domain.VoiceParameters;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class GlobalVolumeRepository : IGlobalVolumeRepository
    {
        readonly IGlobalVolumeStorage _storage;

        GlobalVolume? _globalVolume;

        public GlobalVolumeRepository(IGlobalVolumeStorage storage)
        {
            _storage = storage;
        }

        public async Task<GlobalVolume> GetGlobalVolume()
        {
            if (_globalVolume is not null)
            {
                return _globalVolume;
            }

            var volume = await _storage.FindGlobalVolume();
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
            await _storage.SaveGlobalVolume(volume.Volume.Value);
        }
    }
}
