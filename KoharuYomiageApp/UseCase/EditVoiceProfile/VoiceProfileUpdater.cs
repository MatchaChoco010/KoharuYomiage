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

                VoiceProfileType.MisskeyNote => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyNoteVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveNote => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveNoteVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyRenote => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyRenoteVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveRenote => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveRenoteVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyReactionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyReactionNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveReactionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveReactionNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyReplyNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyReplyNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveReplyNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveReplyNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyRenoteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyRenoteNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveRenoteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveRenoteNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyQuoteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyQuoteNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveQuoteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveQuoteNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyMentionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyMentionNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveMentionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveMentionNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyFollowNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyFollowNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyFollowRequestAcceptedNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyFollowRequestAcceptedNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyReceiveFollowRequestNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyReceiveFollowRequestNotificationVoiceProfile>(id, cancellationToken),

                _ => throw new InvalidProgramException(),
            };

            profile.Update(data.Volume, data.Speed, data.Tone, data.Alpha, data.ToneScale, data.ComponentNormal,
                data.ComponentHappy, data.ComponentAnger, data.ComponentSorrow, data.ComponentCalmness);

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

                VoiceProfileType.MisskeyNote => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyNoteVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveNote => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveNoteVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyRenote => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyRenoteVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveRenote => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveRenoteVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyReactionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyReactionNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveReactionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveReactionNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyReplyNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyReplyNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveReplyNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveReplyNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyRenoteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyRenoteNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveRenoteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveRenoteNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyQuoteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyQuoteNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveQuoteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveQuoteNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyMentionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyMentionNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeySensitiveMentionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveMentionNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyFollowNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyFollowNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyFollowRequestAcceptedNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyFollowRequestAcceptedNotificationVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MisskeyReceiveFollowRequestNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyReceiveFollowRequestNotificationVoiceProfile>(id, cancellationToken),

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

                VoiceProfileType.MisskeyNote => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyNoteVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeySensitiveNote => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveNoteVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeyRenote => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyRenoteVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeySensitiveRenote => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveRenoteVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeyReactionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyReactionNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeySensitiveReactionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveReactionNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeyReplyNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyReplyNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeySensitiveReplyNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveReplyNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeyRenoteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyRenoteNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeySensitiveRenoteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveRenoteNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeyQuoteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyQuoteNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeySensitiveQuoteNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveQuoteNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeyMentionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyMentionNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeySensitiveMentionNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeySensitiveMentionNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeyFollowNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyFollowNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeyFollowRequestAcceptedNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyFollowRequestAcceptedNotificationVoiceProfile>(accountIdentifier, cancellationToken),
                VoiceProfileType.MisskeyReceiveFollowRequestNotification => await _repository
                    .GetVoiceProfile<VoiceProfile.MisskeyReceiveFollowRequestNotificationVoiceProfile>(accountIdentifier, cancellationToken),

                _ => throw new InvalidProgramException(),
            };
            var voiceParameter = await _voiceParameterChangeNotifierRepository.GetInstance(cancellationToken);
            voiceParameter.SetCurrentProfile(profile);
            await _speakText.SpeakText(sampleText, cancellationToken);
        }
    }
}
