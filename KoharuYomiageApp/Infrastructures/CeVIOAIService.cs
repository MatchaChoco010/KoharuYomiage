using System;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using CeVIOAI;
using CeVIOAI.Exceptions;
using KoharuYomiageApp.Application.LoadTalker.Interfaces;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures
{
    public class CeVIOAIService : IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        KoharuRikka? _rikka;

        public CeVIOAIService(LoadTalkerController loadTalkerController, LoadTalkerPresenter loadTalkerPresenter)
        {
            loadTalkerPresenter.OnLoadedWindow
                .Subscribe(async _ =>
                {
                    try
                    {
                        await Task.Run(() => _rikka = new KoharuRikka());
                        loadTalkerController.TalkerLoaded();
                    }
                    catch (Exception e) when (e is DllNotFound or CastNotFound)
                    {
                        // TODO
                    }
                }).AddTo(_disposable);
        }

        void IDisposable.Dispose()
        {
            _rikka?.Dispose();
            _disposable.Dispose();
        }
    }
}
