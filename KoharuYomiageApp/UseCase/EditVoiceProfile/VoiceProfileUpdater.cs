using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.VoiceParameters;
using KoharuYomiageApp.UseCase.EditVoiceProfile.DataObjects;
using KoharuYomiageApp.UseCase.Repository;
using KoharuYomiageApp.UseCase.Utils;

namespace KoharuYomiageApp.UseCase.EditVoiceProfile
{
    public class VoiceProfileUpdater : IVoiceProfileUpdater
    {
        readonly IVoiceProfileRepository _repository;
        readonly ISpeakText _speakText;
        readonly IVoiceParameterChangeNotifierRepository _voiceParameterChangeNotifierRepository;
        readonly IVoiceProfileRepository _voiceProfileRepository;

        public VoiceProfileUpdater(IVoiceProfileRepository repository, ISpeakText speakText,
            IVoiceProfileRepository voiceProfileRepository,
            IVoiceParameterChangeNotifierRepository voiceParameterChangeNotifierRepository)
        {
            _repository = repository;
            _speakText = speakText;
            _voiceParameterChangeNotifierRepository = voiceParameterChangeNotifierRepository;
            _voiceProfileRepository = voiceProfileRepository;
        }

        public async Task SetVoiceProfile(string username, string instance, VoiceProfileType type, VoiceProfileData data, CancellationToken cancellationToken)
        {
            var id = new AccountIdentifier(new Username(username), new Instance(instance));
            var profile = type switch
            {
                VoiceProfileType.MastodonStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonSensitiveStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonBoostedStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonBoostedStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonBoostedSensitiveStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonFollowNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonFollowNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonFollowRequestNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonFollowRequestNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonMentionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonMentionNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonSensitiveMentionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveMentionNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonReblogNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonReblogNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonSensitiveReblogNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveReblogNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonFavoriteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonFavoriteNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonSensitiveFavoriteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveFavoriteNotificationVoiceProfile>(id, cancellationToken),
                _ => throw new InvalidProgramException(),
            };

            profile.Update(data.Volume, data.Speed, data.Tone, data.Alpha, data.ToneScale, data.ComponentNormal,
                data.ComponentHappy, data.ComponentAnger, data.ComponentSorrow, data.ComponentCalmness);

            System.Diagnostics.Debug.WriteLine(profile);

            await _repository.SaveVoiceProfile(profile, cancellationToken);
        }

        public async Task<VoiceProfileData> GetVoiceProfile(string username, string instance, VoiceProfileType type, CancellationToken cancellationToken)
        {
            var id = new AccountIdentifier(new Username(username), new Instance(instance));
            var profile = type switch
            {
                VoiceProfileType.MastodonStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonSensitiveStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonBoostedStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonBoostedStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonBoostedSensitiveStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonFollowNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonFollowNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonFollowRequestNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonFollowRequestNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonMentionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonMentionNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonSensitiveMentionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveMentionNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonReblogNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonReblogNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonSensitiveReblogNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveReblogNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonFavoriteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonFavoriteNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonSensitiveFavoriteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveFavoriteNotificationVoiceProfile>(id, cancellationToken),
                _ => throw new InvalidProgramException(),
            };

            return new VoiceProfileData(profile.Volume, profile.Speed, profile.Tone, profile.Alpha, profile.ToneScale,
                profile.ComponentNormal, profile.ComponentHappy, profile.ComponentAnger, profile.ComponentSorrow,
                profile.ComponentCalmness);
        }

        public async Task PlaySampleVoice(string username, string instance, VoiceProfileType type, string sampleText,
            CancellationToken cancellationToken)
        {
            var accountIdentifier = new AccountIdentifier(new Username(username), new Instance(instance));
            var profile = type switch
            {
                VoiceProfileType.MastodonStatus => await _voiceProfileRepository
                    .GetVoiceProfile<VoiceProfile.MastodonStatusVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MastodonSensitiveStatus => await _voiceProfileRepository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveStatusVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MastodonBoostedStatus => await _voiceProfileRepository
                    .GetVoiceProfile<VoiceProfile.MastodonBoostedStatusVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MastodonBoostedSensitiveStatus => await _voiceProfileRepository
                    .GetVoiceProfile<VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MastodonFollowNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonFollowNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MastodonFollowRequestNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonFollowRequestNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MastodonMentionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonMentionNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MastodonSensitiveMentionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveMentionNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MastodonReblogNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonReblogNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MastodonSensitiveReblogNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveReblogNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MastodonFavoriteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonFavoriteNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MastodonSensitiveFavoriteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveFavoriteNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                _ => throw new InvalidProgramException(),
            };
            var voiceParameter = await _voiceParameterChangeNotifierRepository.GetInstance(cancellationToken);
            voiceParameter.SetCurrentProfile(profile);
            await _speakText.SpeakText(sampleText, cancellationToken);
        }
    }
}
