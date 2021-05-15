using System;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using CeVIOAI;
using KoharuYomiageApp.Application.ReadText.Interfaces;
using KoharuYomiageApp.Application.WindowLoaded.Interfaces;

namespace KoharuYomiageApp.Infrastructures
{
    public class CeVIOAIService : IDisposable, ICeVIOAILoadTalkerService, ICeVIOAISpeakTextService
    {
        readonly CompositeDisposable _disposable = new();

        KoharuRikka? _rikka;

        public async Task LoadTalker()
        {
            await Task.Run(() =>
            {
                _rikka = new KoharuRikka();
            });
        }

        public async Task SpeakText(string text, CancellationToken cancellationToken)
        {
            if (_rikka is null)
            {
                return;
            }

            await _rikka.Speak(text, cancellationToken);
        }

        void IDisposable.Dispose()
        {
            _rikka?.Dispose();
            _disposable.Dispose();
        }
    }
}
