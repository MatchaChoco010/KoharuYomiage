using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.VoiceParameters;
using KoharuYomiageApp.UseCase.Repository;
using ValueTaskSupplement;

namespace KoharuYomiageApp.Data.Repository
{
    public class VoiceParameterChangeNotifierRepository : IDisposable, IVoiceParameterChangeNotifierRepository
    {
        readonly CancellationTokenSource _cancellationTokenSource = new();
        readonly AsyncLazy<VoiceParameterChangeNotifier> _instance;

        public VoiceParameterChangeNotifierRepository(IMastodonAccountRepository mastodonAccountRepository,
            IGlobalVolumeRepository globalVolumeRepository, IVoiceProfileRepository voiceProfileRepository)
        {
            _instance = new AsyncLazy<VoiceParameterChangeNotifier>(async () =>
            {
                var mastodonAccounts =
                    await mastodonAccountRepository.GetAllMastodonAccounts(_cancellationTokenSource.Token);
                var mastodonAccount = mastodonAccounts.FirstOrDefault();
                if (mastodonAccount is not null)
                {
                    var globalVolume = await globalVolumeRepository.GetGlobalVolume(_cancellationTokenSource.Token);
                    var initialCurrentProfile =
                        await voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonStatusVoiceProfile>(
                            mastodonAccount.AccountIdentifier, _cancellationTokenSource.Token);
                    return new VoiceParameterChangeNotifier(initialCurrentProfile, globalVolume);
                }

                // The VoicePlayerChangeNotifierRepository is never instantiated before the Account is created.
                throw new InvalidProgramException();
            });
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
            _instance.AsValueTask().AsTask().Wait();
        }

        public async ValueTask<VoiceParameterChangeNotifier> GetInstance(CancellationToken cancellationToken)
        {
            cancellationToken.Register(_cancellationTokenSource.Cancel);
            return await _instance;
        }
    }
}
