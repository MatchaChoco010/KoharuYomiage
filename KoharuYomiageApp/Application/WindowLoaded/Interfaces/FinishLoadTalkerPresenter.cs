using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.WindowLoaded.UseCases;

namespace KoharuYomiageApp.Application.WindowLoaded.Interfaces
{
    public class FinishLoadTalkerPresenter : IFinishLoadTalker
    {
        readonly Subject<Unit> _onFinishLoadTalker = new();

        public IObservable<Unit> OnFinishLoadTalker => _onFinishLoadTalker;

        public void FinishLoadTalker()
        {
            _onFinishLoadTalker.OnNext(Unit.Default);
        }
    }
}
