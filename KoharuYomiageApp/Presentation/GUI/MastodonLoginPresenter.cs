using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.UseCase.AddMastodonAccount;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MastodonLoginPresenter : IShowAuthUrl, IShowRegisterClientError
    {
        readonly Subject<Uri> _onShowAuthUrl = new();
        readonly Subject<Unit> _onShowRegisterClientError = new();

        public IObservable<Uri> OnShowAuthUrl => _onShowAuthUrl;
        public IObservable<Unit> OnShowRegisterClientError => _onShowRegisterClientError;

        public void ShowAuthUrl(AuthorizationUrl authorizationUrl)
        {
            _onShowAuthUrl.OnNext(authorizationUrl.Url);
        }

        public void ShowRegisterClientError()
        {
            _onShowRegisterClientError.OnNext(Unit.Default);
        }
    }
}
