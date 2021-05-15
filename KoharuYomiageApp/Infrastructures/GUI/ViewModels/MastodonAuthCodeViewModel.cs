using System;
using System.Diagnostics;
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
    public class MastodonAuthCodeViewModel : BindableBase, INavigationAware
    {
        readonly AuthorizeMastodonAccountController _authorizeMastodonAccountController;
        readonly IDialogService _dialogService;
        readonly CompositeDisposable _disposable = new();
        readonly FinishAuthorizeMastodonAccountPresenter _finishAuthorizeMastodonAccountPresenter;
        readonly ShowGetMastodonAccountInfoErrorPresenter _showGetMastodonAccountInfoErrorPresenter;
        readonly ShowMastodonAuthenticationErrorPresenter _showMastodonAuthenticationErrorPresenter;

        public MastodonAuthCodeViewModel(AuthorizeMastodonAccountController authorizeMastodonAccountController,
            ShowMastodonAuthenticationErrorPresenter showMastodonAuthenticationErrorPresenter,
            ShowGetMastodonAccountInfoErrorPresenter showGetMastodonAccountInfoErrorPresenter,
            FinishAuthorizeMastodonAccountPresenter finishAuthorizeMastodonAccountPresenter,
            IDialogService dialogService)
        {
            _authorizeMastodonAccountController = authorizeMastodonAccountController;
            _showMastodonAuthenticationErrorPresenter = showMastodonAuthenticationErrorPresenter;
            _showGetMastodonAccountInfoErrorPresenter = showGetMastodonAccountInfoErrorPresenter;
            _finishAuthorizeMastodonAccountPresenter = finishAuthorizeMastodonAccountPresenter;
            _dialogService = dialogService;
        }

        public ReactiveCommand BackCommand { get; } = new();
        public ReactiveCommand OpenBrowserCommand { get; } = new();
        public ReactiveCommand AuthenticateCommand { get; } = new();
        public ReactivePropertySlim<bool> AuthenticateEnabled { get; } = new();
        public ReactivePropertySlim<string> AuthenticationCode { get; } = new("");

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var authUrl = (navigationContext.Parameters["AuthUrl"] as Uri)!;
            var instanceName = (navigationContext.Parameters["InstanceName"] as string)!;

            BackCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(MastodonLogin)))
                .AddTo(_disposable);
            OpenBrowserCommand.Subscribe(_ => Process.Start(authUrl.ToString())).AddTo(_disposable);
            AuthenticateCommand.Subscribe(_ =>
                {
                    _authorizeMastodonAccountController.AuthorizeMastodonAccount(instanceName,
                        AuthenticationCode.Value);
                    AuthenticateEnabled.Value = false;
                })
                .AddTo(_disposable);
            AuthenticationCode.Subscribe(code => AuthenticateEnabled.Value = !string.IsNullOrWhiteSpace(code))
                .AddTo(_disposable);

            _showMastodonAuthenticationErrorPresenter.OnMastodonAuthenticationError
                .Subscribe(_ =>
                {
                    _dialogService.ShowDialog(nameof(MastodonAuthenticationErrorDialogContent));
                    AuthenticateEnabled.Value = true;
                }).AddTo(_disposable);
            _showGetMastodonAccountInfoErrorPresenter.OnGetMastodonAccountInfoError
                .Subscribe(_ =>
                {
                    _dialogService.ShowDialog(nameof(GetMastodonAccountInfoErrorDialogContent));
                    AuthenticateEnabled.Value = true;
                }).AddTo(_disposable);
            _finishAuthorizeMastodonAccountPresenter.OnFinishAuthorize
                .Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(ViewA)))
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
    }
}
