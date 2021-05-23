using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.VoiceParameters;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class GlobalVolumeRepository : IDisposable, IGlobalVolumeRepository
    {
        readonly IGlobalVolumeStorage _storage;

        GlobalVolume? _globalVolume;

        public GlobalVolumeRepository(IGlobalVolumeStorage storage)
        {
            _storage = storage;
        }

        public async Task<GlobalVolume> GetGlobalVolume(CancellationToken cancellationToken)
        {
            if (_globalVolume is not null)
            {
                return _globalVolume;
            }

            var volume = await _storage.FindGlobalVolume(cancellationToken);
            if (volume is not null)
            {
                _globalVolume = new GlobalVolume(volume.Value);
                return _globalVolume;
            }

            _globalVolume = new GlobalVolume();
            return _globalVolume;
        }

        public async Task SaveGlobalVolume(GlobalVolume volume, CancellationToken cancellationToken)
        {
            await _storage.SaveGlobalVolume(volume.Volume.Value, cancellationToken);
        }

        public void Dispose()
        {
            _globalVolume?.Dispose();
        }
    }
}
