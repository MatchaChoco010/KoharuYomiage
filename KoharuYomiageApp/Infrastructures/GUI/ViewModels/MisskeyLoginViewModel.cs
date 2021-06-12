using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
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
    public class MisskeyLoginViewModel : BindableBase, INavigationAware, IDisposable
    {
        readonly IDialogService _dialogService;
        readonly CompositeDisposable _disposable = new();
        readonly MisskeyLoginController _misskeyLoginController;
        readonly MisskeyLoginPresenter _misskeyLoginPresenter;

        bool _loginPhase = true;
        Uri? _authorizeUrl;

        public MisskeyLoginViewModel(MisskeyLoginController misskeyLoginController,
            MisskeyLoginPresenter misskeyLoginPresenter, IDialogService dialogService)
        {
            _misskeyLoginController = misskeyLoginController;
            _misskeyLoginPresenter = misskeyLoginPresenter;
            _dialogService = dialogService;
        }

        public ReactivePropertySlim<string> InstanceName { get; } = new("");
        public ReactivePropertySlim<bool> LoginEnabled { get; } = new();
        public ReactivePropertySlim<bool> OpenAuthorizeUrlEnabled { get; } = new();
        public ReactivePropertySlim<bool> FinishAuthorizeEnabled { get; } = new();
        public ReactiveCommand LoginCommand { get; } = new();
        public ReactiveCommand OpenAuthorizeUrlCommand { get; } = new();
        public ReactiveCommand FinishAuthorizeCommand { get; } = new();
        public ReactiveCommand BackCommand { get; } = new();

        public void Dispose()
        {
            _disposable.Dispose();
            InstanceName.Dispose();
            LoginEnabled.Dispose();
            OpenAuthorizeUrlEnabled.Dispose();
            FinishAuthorizeEnabled.Dispose();
            LoginCommand.Dispose();
            BackCommand.Dispose();
            OpenAuthorizeUrlCommand.Dispose();
            FinishAuthorizeCommand.Dispose();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var isFirstLogin = navigationContext.Parameters["FirstLogin"] as bool? ?? false;
            _loginPhase = true;

            LoginCommand.Select(_ => Observable.StartAsync(async cancellationToken =>
                {
                    _loginPhase = false;
                    LoginEnabled.Value = _loginPhase;
                    await _misskeyLoginController.LoginMisskeyAccount(InstanceName.Value, cancellationToken);
                    navigationContext.NavigationService.RequestNavigate(nameof(MainControl));
                }))
                .Switch()
                .Subscribe()
                .AddTo(_disposable);

            _misskeyLoginPresenter.OnShowAuthUrl
                .Subscribe(authUrl =>
                {
                    _authorizeUrl = authUrl;
                    OpenAuthorizeUrlEnabled.Value = true;
                    FinishAuthorizeEnabled.Value = true;
                })
                .AddTo(_disposable);

            BackCommand.Subscribe(_ =>
                    navigationContext.NavigationService.RequestNavigate(nameof(SelectSNS),
                        new NavigationParameters { { "FirstLogin", isFirstLogin } }))
                .AddTo(_disposable);

            InstanceName.Subscribe(text => LoginEnabled.Value = IsInstanceName(text) && _loginPhase)
                .AddTo(_disposable);

            OpenAuthorizeUrlCommand.Subscribe(_ => Process.Start(_authorizeUrl?.ToString() ?? "")).AddTo(_disposable);

            FinishAuthorizeCommand.Subscribe(_misskeyLoginPresenter.FinishAuthorize).AddTo(_disposable);

            _misskeyLoginPresenter.OnShowRegisterClientError
                .Subscribe(_ =>
                {
                    _dialogService.ShowDialog(nameof(RegisterClientError));
                    _loginPhase = true;
                    LoginEnabled.Value = _loginPhase;
                })
                .AddTo(_disposable);
            _misskeyLoginPresenter.OnShowAuthorizeError
                .Subscribe(_ =>
                {
                    _dialogService.ShowDialog(nameof(RegisterClientError));
                    _loginPhase = true;
                    LoginEnabled.Value = _loginPhase;
                })
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

        static bool IsInstanceName(string accountText)
        {
            return Uri.CheckHostName(accountText) is not UriHostNameType.Unknown;
        }
    }
}
