using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.ReadingText;
using KoharuYomiageApp.Domain.VoiceParameters;
using KoharuYomiageApp.UseCase.Repository;

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
