using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.UseCase.WindowLoaded;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class StartPresenter : IStartApp, IStartRegisteringAccount, IFinishLoadTalker, IShowLoadTalkerError
    {
        readonly Subject<Unit> _onFinishLoadTalker = new();
        readonly Subject<Unit> _onShowLoadTalkerError = new();
        readonly Subject<Unit> _onStartApp = new();
        readonly Subject<Unit> _onStartRegisterAccount = new();

        public IObservable<Unit> OnFinishLoadTalker => _onFinishLoadTalker;
        public IObservable<Unit> OnStartApp => _onStartApp;
        public IObservable<Unit> OnStartRegisterAccount => _onStartRegisterAccount;
        public IObservable<Unit> OnShowLoadTalkerError => _onShowLoadTalkerError;

        public void FinishLoadTalker()
        {
            _onFinishLoadTalker.OnNext(Unit.Default);
        }

        public void ShowLoadTalkerError()
        {
            _onShowLoadTalkerError.OnNext(Unit.Default);
        }

        public void StartApp()
        {
            _onStartApp.OnNext(Unit.Default);
        }

        public void StartRegisteringAccount()
        {
            _onStartRegisterAccount.OnNext(Unit.Default);
        }
    }
}
