using System.Text.RegularExpressions;
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
            string filteredText = text;
            filteredText = Regex.Replace(filteredText, "<br[^>]*?>", "\n");
            filteredText = Regex.Replace(filteredText, "</p>", "</p>\n\n");
            filteredText = Regex.Replace(filteredText, "<[^>]*?>", "");
            filteredText = Regex.Replace(filteredText, @"https?://[\w/:%#\$&\?\(\)~\.=\+\-]+", "URL");
            await _speakTextService.SpeakText(filteredText, cancellationToken);
        }
    }
}
