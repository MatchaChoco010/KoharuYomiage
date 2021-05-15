using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public class ShowMastodonAuthenticationErrorPresenter : IShowMastodonAuthenticationError
    {
        readonly Subject<Unit> onMastodonAuthenticationError = new();

        public IObservable<Unit> OnMastodonAuthenticationError => onMastodonAuthenticationError;

        public void ShowMastodonAuthenticationError()
        {
            onMastodonAuthenticationError.OnNext(Unit.Default);
        }
    }
}
