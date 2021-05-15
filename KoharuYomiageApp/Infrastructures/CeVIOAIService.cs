using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using CeVIOAI;
using KoharuYomiageApp.Application.WindowLoaded.Interfaces;

namespace KoharuYomiageApp.Infrastructures
{
    public class CeVIOAIService : IDisposable, ICeVIOAILoadTalkerService
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

        void IDisposable.Dispose()
        {
            _rikka?.Dispose();
            _disposable.Dispose();
        }
    }
}
