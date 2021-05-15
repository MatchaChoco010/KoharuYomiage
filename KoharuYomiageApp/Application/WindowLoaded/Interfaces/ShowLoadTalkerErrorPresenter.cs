using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.WindowLoaded.UseCases;

namespace KoharuYomiageApp.Application.WindowLoaded.Interfaces
{
    public class ShowLoadTalkerErrorPresenter : IShowLoadTalkerError
    {
        readonly Subject<Unit> _onShowLoadTalkerError = new();

        public IObservable<Unit> OnShowLoadTalkerError => _onShowLoadTalkerError;

        public void ShowLoadTalkerError()
        {
            _onShowLoadTalkerError.OnNext(Unit.Default);
        }
    }
}
