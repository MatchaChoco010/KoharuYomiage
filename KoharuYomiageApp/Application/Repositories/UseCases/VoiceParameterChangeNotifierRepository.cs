using System;
using System.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.Entities.VoiceParameters;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public class VoiceParameterChangeNotifierRepository
    {
        readonly GlobalVolumeRepository _globalVolumeRepository;
        readonly MastodonAccountRepository _mastodonAccountRepository;
        readonly VoiceProfileRepository _voiceProfileRepository;

        VoiceParameterChangeNotifier? _instance;

        public VoiceParameterChangeNotifierRepository(MastodonAccountRepository mastodonAccountRepository,
            GlobalVolumeRepository globalVolumeRepository, VoiceProfileRepository voiceProfileRepository)
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

            var mastodonAccounts = await _mastodonAccountRepository.GetMastodonAccounts();
            var mastodonAccount = mastodonAccounts.FirstOrDefault();
            if (mastodonAccount is not null)
            {
                var globalVolume = await _globalVolumeRepository.GetGlobalVolume();
                var initialCurrentProfile =
                    await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonStatusVoiceProfile>(
                        mastodonAccount
                            .AccountIdentifier);
                _instance = new VoiceParameterChangeNotifier(initialCurrentProfile, globalVolume);
                return _instance;
            }

            throw new InvalidProgramException();
        }
    }
}
