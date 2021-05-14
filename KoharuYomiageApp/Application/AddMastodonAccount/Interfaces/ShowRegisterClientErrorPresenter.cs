using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public class ShowRegisterClientErrorPresenter : IShowRegisterClientError
    {
        readonly Subject<Unit> onShowRegisterClientError = new();

        public IObservable<Unit> OnShowRegisterClientError => onShowRegisterClientError;

        public void ShowRegisterClientError()
        {
            onShowRegisterClientError.OnNext(Unit.Default);
        }
    }
}
