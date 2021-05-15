using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public class FinishAuthorizeMastodonAccountPresenter : IFinishAuthorizeMastodonAccount
    {
        readonly Subject<Unit> _onFinishAuthorize = new();

        public IObservable<Unit> OnFinishAuthorize => _onFinishAuthorize;

        public void FinishAuthorizeMastodonAccount()
        {
            _onFinishAuthorize.OnNext(Unit.Default);
        }
    }
}
