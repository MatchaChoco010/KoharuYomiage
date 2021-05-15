using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.WindowLoaded.UseCases;

namespace KoharuYomiageApp.Application.WindowLoaded.Interfaces
{
    public class StartRegisteringAccountPresenter : IStartRegisteringAccount
    {
        readonly Subject<Unit> _onStartRegisterAccount = new();

        public IObservable<Unit> OnStartRegisterAccount => _onStartRegisterAccount;

        public void StartRegisteringAccount()
        {
            _onStartRegisterAccount.OnNext(Unit.Default);
        }
    }
}
