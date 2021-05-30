using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using KoharuYomiageApp.Infrastructures.GUI.Views.Dialogs;
using KoharuYomiageApp.Presentation.GUI;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class MastodonAuthCodeViewModel : BindableBase, INavigationAware, IDisposable
    {
        readonly IDialogService _dialogService;
        readonly CompositeDisposable _disposable = new();
        readonly MastodonAuthCodeController _mastodonAuthCodeController;
        readonly MastodonAuthCodePresenter _mastodonAuthCodePresenter;

        public MastodonAuthCodeViewModel(MastodonAuthCodePresenter mastodonAuthCodePresenter,
            MastodonAuthCodeController mastodonAuthCodeController, IDialogService dialogService)
        {
            _mastodonAuthCodePresenter = mastodonAuthCodePresenter;
            _mastodonAuthCodeController = mastodonAuthCodeController;
            _dialogService = dialogService;
        }

        public ReactiveCommand BackCommand { get; } = new();
        public ReactiveCommand OpenBrowserCommand { get; } = new();
        public ReactiveCommand AuthenticateCommand { get; } = new();
        public ReactivePropertySlim<bool> AuthenticateEnabled { get; } = new();
        public ReactivePropertySlim<string> AuthenticationCode { get; } = new("");

        public void Dispose()
        {
            _disposable.Dispose();
            BackCommand.Dispose();
            OpenBrowserCommand.Dispose();
            AuthenticateCommand.Dispose();
            AuthenticateEnabled.Dispose();
            AuthenticationCode.Dispose();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var authUrl = (navigationContext.Parameters["AuthUrl"] as Uri)!;
            var instanceName = (navigationContext.Parameters["InstanceName"] as string)!;

            BackCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(MastodonLogin)))
                .AddTo(_disposable);
            OpenBrowserCommand.Subscribe(_ => Process.Start(authUrl.ToString())).AddTo(_disposable);
            AuthenticateCommand.Subscribe(_ =>
                {
                    _mastodonAuthCodeController.AuthorizeMastodonAccount(instanceName,
                        AuthenticationCode.Value);
                    AuthenticateEnabled.Value = false;
                })
                .AddTo(_disposable);
            AuthenticationCode.Subscribe(code => AuthenticateEnabled.Value = !string.IsNullOrWhiteSpace(code))
                .AddTo(_disposable);

            _mastodonAuthCodePresenter.OnMastodonAuthenticationError
                .Subscribe(_ =>
                {
                    _dialogService.ShowDialog(nameof(MastodonAuthenticationError));
                    AuthenticateEnabled.Value = true;
                }).AddTo(_disposable);
            _mastodonAuthCodePresenter.OnGetMastodonAccountInfoError
                .Subscribe(_ =>
                {
                    _dialogService.ShowDialog(nameof(GetMastodonAccountInfoError));
                    AuthenticateEnabled.Value = true;
                }).AddTo(_disposable);
            _mastodonAuthCodePresenter.OnFinishAuthorize
                .Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(MainControl)))
                .AddTo(_disposable);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _disposable.Clear();
        }
    }
}
