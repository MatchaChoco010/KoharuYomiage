using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using CeVIOAI;
using KoharuYomiageApp.Presentation.CeVIOAI;
using KoharuYomiageApp.Presentation.CeVIOAI.DataObjects;

namespace KoharuYomiageApp.Infrastructures.CeVIOAI
{
    public class CeVIOAIHost : IDisposable, ICeVIOAILoadTalker, ICeVIOAISpeakText,
        ICeVIOAIUpdateVoiceParameter
    {
        readonly CompositeDisposable _disposable = new();

        KoharuRikka? _rikka;

        public async Task LoadTalker(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                _rikka = new KoharuRikka();
            }, cancellationToken);
        }

        public async Task SpeakText(string text, CancellationToken cancellationToken)
        {
            if (_rikka is null)
            {
                return;
            }

            await _rikka.Speak(text, cancellationToken);
        }

        public void Update(VoiceParameterOutputData data)
        {
            if (_rikka is null)
            {
                return;
            }

            _rikka.GlobalVolume = data.GlobalVolume;
            _rikka.Volume = data.Volume;
            _rikka.Speed = data.Speed;
            _rikka.Tone = data.Tone;
            _rikka.Alpha = data.Alpha;
            _rikka.ToneScale = data.ToneScale;
            _rikka.ComponentNormal = data.ComponentNormal;
            _rikka.ComponentHappy = data.ComponentHappy;
            _rikka.ComponentAnger = data.ComponentAnger;
            _rikka.ComponentSorrow = data.ComponentSorrow;
            _rikka.ComponentCalmness = data.ComponentCalmness;
        }

        void IDisposable.Dispose()
        {
            _rikka?.Dispose();
            _disposable.Dispose();
        }
    }
}
