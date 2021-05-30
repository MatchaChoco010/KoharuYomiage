using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.VoiceParameters;
using KoharuYomiageApp.UseCase.Repository;
using ValueTaskSupplement;

namespace KoharuYomiageApp.Data.Repository
{
    public class VoiceParameterChangeNotifierRepository : IDisposable, IVoiceParameterChangeNotifierRepository
    {
        readonly CancellationTokenSource _cancellationTokenSource = new();
        readonly AsyncLazy<VoiceParameterChangeNotifier> _instance;

        public VoiceParameterChangeNotifierRepository(IGlobalVolumeRepository globalVolumeRepository,
            IVoiceProfileRepository voiceProfileRepository)
        {
            _instance = new AsyncLazy<VoiceParameterChangeNotifier>(async () =>
            {
                var globalVolume = await globalVolumeRepository.GetGlobalVolume(_cancellationTokenSource.Token);
                return new VoiceParameterChangeNotifier(globalVolume);
            });
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel(true);
            _cancellationTokenSource.Dispose();
        }

        public async ValueTask<VoiceParameterChangeNotifier> GetInstance(CancellationToken cancellationToken)
        {
            cancellationToken.Register(_cancellationTokenSource.Cancel);
            return await _instance;
        }
    }
}
