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
        readonly Subject<Unit> onGetMastodonAccountInfoError = new();
        readonly Subject<Unit> onMastodonAuthenticationError = new();

        public IObservable<Unit> OnFinishAuthorize => _onFinishAuthorize;
        public IObservable<Unit> OnGetMastodonAccountInfoError => onGetMastodonAccountInfoError;
        public IObservable<Unit> OnMastodonAuthenticationError => onMastodonAuthenticationError;

        public void FinishAuthorizeMastodonAccount()
        {
            _onFinishAuthorize.OnNext(Unit.Default);
        }

        public void ShowGetMastodonAccountInfoError()
        {
            onGetMastodonAccountInfoError.OnNext(Unit.Default);
        }

        public void ShowMastodonAuthenticationError()
        {
            onMastodonAuthenticationError.OnNext(Unit.Default);
        }
    }
}
