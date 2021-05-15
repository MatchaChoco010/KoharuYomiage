using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public class ShowGetMastodonAccountInfoErrorPresenter : IShowGetMastodonAccountInfoError
    {
        readonly Subject<Unit> onGetMastodonAccountInfoError = new();

        public IObservable<Unit> OnGetMastodonAccountInfoError => onGetMastodonAccountInfoError;

        public void ShowGetMastodonAccountInfoError()
        {
            onGetMastodonAccountInfoError.OnNext(Unit.Default);
        }
    }
}
