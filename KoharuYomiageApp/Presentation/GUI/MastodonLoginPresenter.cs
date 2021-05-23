using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.UseCase.AddMastodonAccount;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MastodonLoginPresenter : IShowAuthUrl, IShowRegisterClientError
    {
        readonly Subject<Uri> onShowAuthUrl = new();
        readonly Subject<Unit> onShowRegisterClientError = new();

        public IObservable<Uri> OnShowAuthUrl => onShowAuthUrl;
        public IObservable<Unit> OnShowRegisterClientError => onShowRegisterClientError;

        public void ShowAuthUrl(AuthorizationUrl authorizationUrl)
        {
            onShowAuthUrl.OnNext(authorizationUrl.Url);
        }

        public void ShowRegisterClientError()
        {
            onShowRegisterClientError.OnNext(Unit.Default);
        }
    }
}
