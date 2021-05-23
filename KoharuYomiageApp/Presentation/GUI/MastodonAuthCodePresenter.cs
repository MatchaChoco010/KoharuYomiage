using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.UseCase.AddMastodonAccount;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MastodonAuthCodePresenter : IFinishAuthorizeMastodonAccount, IShowGetMastodonAccountInfoError,
        IShowMastodonAuthenticationError
    {
        readonly Subject<Unit> _onFinishAuthorize = new();
        readonly Subject<Unit> _onGetMastodonAccountInfoError = new();
        readonly Subject<Unit> _onMastodonAuthenticationError = new();

        public IObservable<Unit> OnFinishAuthorize => _onFinishAuthorize;
        public IObservable<Unit> OnGetMastodonAccountInfoError => _onGetMastodonAccountInfoError;
        public IObservable<Unit> OnMastodonAuthenticationError => _onMastodonAuthenticationError;

        public void FinishAuthorizeMastodonAccount()
        {
            _onFinishAuthorize.OnNext(Unit.Default);
        }

        public void ShowGetMastodonAccountInfoError()
        {
            _onGetMastodonAccountInfoError.OnNext(Unit.Default);
        }

        public void ShowMastodonAuthenticationError()
        {
            _onMastodonAuthenticationError.OnNext(Unit.Default);
        }
    }
}
