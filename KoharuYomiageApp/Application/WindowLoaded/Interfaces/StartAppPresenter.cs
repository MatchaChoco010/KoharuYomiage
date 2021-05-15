using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.WindowLoaded.UseCases;

namespace KoharuYomiageApp.Application.WindowLoaded.Interfaces
{
    public class StartAppPresenter : IStartApp
    {
        readonly Subject<Unit> _onStartApp = new();

        public IObservable<Unit> OnStartApp => _onStartApp;

        public void StartApp()
        {
            _onStartApp.OnNext(Unit.Default);
        }
    }
}
