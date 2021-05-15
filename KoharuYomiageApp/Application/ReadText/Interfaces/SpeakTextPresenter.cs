using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.ReadText.UseCases;

namespace KoharuYomiageApp.Application.ReadText.Interfaces
{
    public class SpeakTextPresenter : ISpeakText
    {
        readonly ICeVIOAISpeakTextService _speakTextService;

        public SpeakTextPresenter(ICeVIOAISpeakTextService speakTextService)
        {
            _speakTextService = speakTextService;
        }

        public async Task SpeakText(string text, CancellationToken cancellationToken)
        {
            await _speakTextService.SpeakText(text, cancellationToken);
        }
    }
}
