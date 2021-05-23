using System;
using System.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.VoiceParameters;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class VoiceParameterChangeNotifierRepository : IDisposable, IVoiceParameterChangeNotifierRepository
    {
        readonly IGlobalVolumeRepository _globalVolumeRepository;
        readonly IMastodonAccountRepository _mastodonAccountRepository;
        readonly IVoiceProfileRepository _voiceProfileRepository;

        VoiceParameterChangeNotifier? _instance;

        public VoiceParameterChangeNotifierRepository(IMastodonAccountRepository mastodonAccountRepository,
            IGlobalVolumeRepository globalVolumeRepository, IVoiceProfileRepository voiceProfileRepository)
        {
            _mastodonAccountRepository = mastodonAccountRepository;
            _globalVolumeRepository = globalVolumeRepository;
            _voiceProfileRepository = voiceProfileRepository;
        }

        public async ValueTask<VoiceParameterChangeNotifier> GetInstance()
        {
            if (_instance is not null)
            {
                return _instance;
            }

            var mastodonAccounts = await _mastodonAccountRepository.GetAllMastodonAccounts();
            var mastodonAccount = mastodonAccounts.FirstOrDefault();
            if (mastodonAccount is not null)
            {
                var globalVolume = await _globalVolumeRepository.GetGlobalVolume();
                var initialCurrentProfile =
                    await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonStatusVoiceProfile>(
                        mastodonAccount.AccountIdentifier);
                _instance = new VoiceParameterChangeNotifier(initialCurrentProfile, globalVolume);
                return _instance;
            }

            // The VoicePlayerChangeNotifierRepository is never instantiated before the Account is created.
            throw new InvalidProgramException();
        }

        public void Dispose()
        {
            _instance?.Dispose();
        }
    }
}
