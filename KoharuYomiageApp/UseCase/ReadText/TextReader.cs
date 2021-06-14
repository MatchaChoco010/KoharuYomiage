using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;
using KoharuYomiageApp.Domain.VoiceParameters;
using KoharuYomiageApp.UseCase.Repository;
using KoharuYomiageApp.UseCase.Utils;

namespace KoharuYomiageApp.UseCase.ReadText
{
    public class TextReader : IStartReading
    {
        readonly IChangeImage _changeImage;
        readonly IReadingTextContainerRepository _containerRepository;
        readonly ISpeakText _speakText;
        readonly IVoiceParameterChangeNotifierRepository _voiceParameterChangeNotifierRepository;
        readonly IVoiceProfileRepository _voiceProfileRepository;


        public TextReader(IReadingTextContainerRepository containerRepository,
            IVoiceProfileRepository voiceProfileRepository,
            IVoiceParameterChangeNotifierRepository voiceParameterChangeNotifierRepository,
            ISpeakText speakText, IChangeImage changeImage)
        {
            _containerRepository = containerRepository;
            _voiceParameterChangeNotifierRepository = voiceParameterChangeNotifierRepository;
            _voiceProfileRepository = voiceProfileRepository;
            _speakText = speakText;
            _changeImage = changeImage;
        }

        public async Task StartReading(CancellationToken cancellationToken)
        {
            var container = _containerRepository.GetContainer();
            var voiceParameter = await _voiceParameterChangeNotifierRepository.GetInstance(cancellationToken);

            while (!cancellationToken.IsCancellationRequested)
            {
                var item = await container.PeekAsync(cancellationToken);
                var tmp = item.AccountIdentifier.Value.Split('@');
                var username = tmp[0]!;
                var instance = tmp[1]!;
                var accountIdentifier = new AccountIdentifier(new Username(username), new Instance(instance));

                VoiceProfile profile;
                switch (item)
                {
                    case ReadingTextItem.MastodonStatusReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonStatusVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MastodonSensitiveStatusReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonSensitiveStatusVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MastodonBoostedStatusReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonBoostedStatusVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MastodonBoostedSensitiveStatusReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MastodonFollowNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonFollowNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MastodonFollowRequestNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonFollowRequestNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MastodonMentionNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonMentionNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MastodonSensitiveMentionNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonSensitiveMentionNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MastodonReblogNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonReblogNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MastodonSensitiveReblogNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonSensitiveReblogNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MastodonFavoriteNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonFavoriteNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MastodonSensitiveFavoriteNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MastodonSensitiveFavoriteNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;

                    case ReadingTextItem.MisskeyNoteReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeyNoteVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeySensitiveNoteReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeySensitiveNoteVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeyRenoteReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeyRenoteVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeySensitiveRenoteReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeySensitiveRenoteVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeyReactionNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeyReactionNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeySensitiveReactionNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeySensitiveReactionNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeyReplyNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeyReplyNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeySensitiveReplyNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeySensitiveReplyNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeyRenoteNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeyRenoteNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeySensitiveRenoteNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeySensitiveRenoteNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeyQuoteNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeyQuoteNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeySensitiveQuoteNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeySensitiveQuoteNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeyMentionNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeyMentionNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeySensitiveMentionNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeySensitiveMentionNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeyFollowNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeyFollowNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeyFollowRequestAcceptedNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeyFollowRequestAcceptedNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                    case ReadingTextItem.MisskeyReceiveFollowRequestNotificationReadingTextItem:
                        profile =
                            await _voiceProfileRepository.GetVoiceProfile<VoiceProfile.MisskeyReceiveFollowRequestNotificationVoiceProfile>(
                                accountIdentifier, cancellationToken);
                        voiceParameter.SetCurrentProfile(profile);
                        break;
                }

                try
                {
                    _changeImage.OpenMouth();
                    var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    await Task.WhenAny(_speakText.SpeakText(item.Text, cts.Token), container.Overflow(cts.Token));
                    cts.Cancel(true);
                    cts.Dispose();
                }
                finally
                {
                    _changeImage.CloseMouth();
                    container.RemoveFirstItem();
                }
            }
        }
    }
}
