using System;
using System.Reactive.Disposables;
using KoharuYomiageApp.Application.AddMastodonAccount.Interfaces;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class MastodonLoginViewModel : BindableBase, INavigationAware
    {
        readonly IDialogService _dialogService;
        readonly CompositeDisposable _disposable = new();
        readonly LoginMastodonAccountController _loginMastodonAccountController;
        readonly ShowAuthUrlPresenter _showAuthUrlPresenter;
        readonly ShowRegisterClientErrorPresenter _showRegisterClientErrorPresenter;

        public MastodonLoginViewModel(LoginMastodonAccountController loginMastodonAccountController,
            ShowRegisterClientErrorPresenter showRegisterClientErrorPresenter,
            ShowAuthUrlPresenter showAuthUrlPresenter, IDialogService dialogService)
        {
            _loginMastodonAccountController = loginMastodonAccountController;
            _showRegisterClientErrorPresenter = showRegisterClientErrorPresenter;
            _showAuthUrlPresenter = showAuthUrlPresenter;
            _dialogService = dialogService;
        }

        public ReactivePropertySlim<string> InstanceName { get; } = new("");
        public ReactivePropertySlim<bool> LoginEnabled { get; } = new();
        public ReactiveCommand LoginCommand { get; } = new();
        public ReactiveCommand BackCommand { get; } = new();

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var isFirstLogin = navigationContext.Parameters["FirstLogin"] as bool? ?? false;

            LoginCommand.Subscribe(_ =>
                {
                    LoginEnabled.Value = false;
                    _loginMastodonAccountController.LoginMastodonAccount(InstanceName.Value);
                })
                .AddTo(_disposable);
            BackCommand.Subscribe(_ =>
                    navigationContext.NavigationService.RequestNavigate(nameof(SelectSNS),
                        new NavigationParameters {{"FirstLogin", isFirstLogin}}))
                .AddTo(_disposable);
            InstanceName.Subscribe(text => LoginEnabled.Value = IsInstanceName(text)).AddTo(_disposable);

            _showRegisterClientErrorPresenter.OnShowRegisterClientError
                .Subscribe(_ =>
                {
                    _dialogService.ShowDialog(nameof(RegisterClientErrorDialogContent));
                    LoginEnabled.Value = true;
                })
                .AddTo(_disposable);
            _showAuthUrlPresenter.OnShowAuthUrl
                .Subscribe(authUrl =>
                    navigationContext.NavigationService.RequestNavigate(nameof(MastodonAuthCode),
                        new NavigationParameters {{"AuthUrl", authUrl}, {"InstanceName", InstanceName.Value}}))
                .AddTo(_disposable);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _disposable.Clear();
        }

        static bool IsInstanceName(string accountText)
        {
            return Uri.CheckHostName(accountText) is not UriHostNameType.Unknown;
        }
    }
}
