using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.VoiceParameters;
using KoharuYomiageApp.UseCase.Repository;
using ValueTaskSupplement;

namespace KoharuYomiageApp.Data.Repository
{
    public class GlobalVolumeRepository : IDisposable, IGlobalVolumeRepository
    {
        readonly CancellationTokenSource _cancellationTokenSource = new();
        readonly IGlobalVolumeStorage _storage;
        readonly AsyncLazy<GlobalVolume> _globalVolume;

        public GlobalVolumeRepository(IGlobalVolumeStorage storage)
        {
            _storage = storage;
            _globalVolume = ValueTaskEx.Lazy(async () =>
            {
                var volume = await _storage.FindGlobalVolume(_cancellationTokenSource.Token);
                return volume is not null ? new GlobalVolume(volume.Value) : new GlobalVolume();
            });
        }

        public async Task<GlobalVolume> GetGlobalVolume(CancellationToken cancellationToken)
        {
            cancellationToken.Register(_cancellationTokenSource.Cancel);
            return await _globalVolume;
        }

        public async Task SaveGlobalVolume(GlobalVolume volume, CancellationToken cancellationToken)
        {
            await _storage.SaveGlobalVolume(volume.Volume.Value, cancellationToken);
        }

        public void Dispose()
        {
            _globalVolume.AsValueTask().AsTask().Wait();
        }
    }
}
