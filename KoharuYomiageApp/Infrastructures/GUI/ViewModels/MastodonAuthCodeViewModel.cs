using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class MastodonAuthCodeViewModel : BindableBase, INavigationAware
    {
        readonly CompositeDisposable _disposable = new();

        Uri? _authUrl;

        public ReactiveCommand BackCommand { get; } = new();
        public ReactiveCommand OpenBrowserCommand { get; } = new();
        public ReactiveCommand AuthenticateCommand { get; } = new();
        public ReactivePropertySlim<bool> AuthenticateEnabled { get; } = new();
        public ReactivePropertySlim<string> AuthenticationCode { get; } = new("");

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _authUrl = navigationContext.Parameters["AuthUrl"] as Uri;
            BackCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(MastodonLogin)))
                .AddTo(_disposable);
            OpenBrowserCommand.Subscribe(_ =>
            {
                if (_authUrl is not null)
                {
                    Process.Start(_authUrl.ToString());
                }
            }).AddTo(_disposable);
            AuthenticationCode.Subscribe(code => AuthenticateEnabled.Value = !string.IsNullOrWhiteSpace(code))
                .AddTo(_disposable);
            AuthenticateCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(ViewA)))
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
