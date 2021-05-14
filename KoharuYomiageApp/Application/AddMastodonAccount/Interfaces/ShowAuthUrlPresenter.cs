using System;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonAccount.Interfaces
{
    public class ShowAuthUrlPresenter : IShowAuthUrl
    {
        readonly Subject<Uri> onShowAuthUrl = new();

        public IObservable<Uri> OnShowAuthUrl => onShowAuthUrl;

        public void ShowAuthUrl(AuthUrl authUrl)
        {
            onShowAuthUrl.OnNext(authUrl.Url);
        }
    }
}
