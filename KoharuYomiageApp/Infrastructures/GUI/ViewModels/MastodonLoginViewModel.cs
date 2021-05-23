using System;
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
    public class MastodonLoginViewModel : BindableBase, INavigationAware, IDisposable
    {
        readonly IDialogService _dialogService;
        readonly CompositeDisposable _disposable = new();
        readonly MastodonLoginController _mastodonLoginController;
        readonly MastodonLoginPresenter _mastodonLoginPresenter;

        public MastodonLoginViewModel(MastodonLoginPresenter mastodonLoginPresenter,
            MastodonLoginController mastodonLoginController, IDialogService dialogService)
        {
            _mastodonLoginPresenter = mastodonLoginPresenter;
            _mastodonLoginController = mastodonLoginController;
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
                    _mastodonLoginController.LoginMastodonAccount(InstanceName.Value);
                })
                .AddTo(_disposable);
            BackCommand.Subscribe(_ =>
                    navigationContext.NavigationService.RequestNavigate(nameof(SelectSNS),
                        new NavigationParameters {{"FirstLogin", isFirstLogin}}))
                .AddTo(_disposable);
            InstanceName.Subscribe(text => LoginEnabled.Value = IsInstanceName(text)).AddTo(_disposable);

            _mastodonLoginPresenter.OnShowRegisterClientError
                .Subscribe(_ =>
                {
                    _dialogService.ShowDialog(nameof(RegisterClientError));
                    LoginEnabled.Value = true;
                })
                .AddTo(_disposable);
            _mastodonLoginPresenter.OnShowAuthUrl
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

        public void Dispose()
        {
            _disposable.Dispose();
            _mastodonLoginController.Dispose();
            InstanceName.Dispose();
            LoginEnabled.Dispose();
            LoginCommand.Dispose();
            BackCommand.Dispose();
        }
    }
}
