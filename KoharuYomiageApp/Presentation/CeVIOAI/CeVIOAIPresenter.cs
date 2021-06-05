using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Presentation.CeVIOAI.DataObjects;
using KoharuYomiageApp.UseCase.ReadText;
using KoharuYomiageApp.UseCase.UpdateVoiceParameter;
using KoharuYomiageApp.UseCase.UpdateVoiceParameter.DataObjects;
using KoharuYomiageApp.UseCase.Utils;
using KoharuYomiageApp.UseCase.WindowLoaded;

namespace KoharuYomiageApp.Presentation.CeVIOAI
{
    public class CeVIOAIPresenter : ILoadTalker, ISpeakText, IUpdateVoiceParameter
    {
        readonly ICeVIOAILoadTalker _loadTalker;
        readonly ICeVIOAISpeakText _speakText;
        readonly ICeVIOAIUpdateVoiceParameter _updateVoiceParameter;

        public CeVIOAIPresenter(ICeVIOAILoadTalker loadTalker, ICeVIOAISpeakText speakText,
            ICeVIOAIUpdateVoiceParameter updateVoiceParameter)
        {
            _loadTalker = loadTalker;
            _speakText = speakText;
            _updateVoiceParameter = updateVoiceParameter;
        }

        public async Task LoadTalker(CancellationToken cancellationToken)
        {
            await _loadTalker.LoadTalker(cancellationToken);
        }

        public async Task SpeakText(string text, CancellationToken cancellationToken)
        {
            string filteredText = text;
            filteredText = Regex.Replace(filteredText, "<br[^>]*?>", "\n");
            filteredText = Regex.Replace(filteredText, "</p>", "</p>\n\n");
            filteredText = Regex.Replace(filteredText, "<[^>]*?>", "");
            filteredText = Regex.Replace(filteredText, @"https?://[\w/:%#\$&\?\(\)~\.=\+\-]+", "URL");
            await _speakText.SpeakText(filteredText, cancellationToken);
        }

        public void Update(VoiceParameterData data)
        {
            _updateVoiceParameter.Update(new VoiceParameterOutputData(data.Volume, data.Speed, data.Tone, data.Alpha,
                data.ToneScale, data.ComponentNormal, data.ComponentHappy, data.ComponentAnger, data.ComponentSorrow,
                data.ComponentCalmness));
        }
    }
}
