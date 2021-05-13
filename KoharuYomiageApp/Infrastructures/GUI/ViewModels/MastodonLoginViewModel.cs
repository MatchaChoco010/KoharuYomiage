using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using System.Text.RegularExpressions;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class MastodonLoginViewModel : BindableBase, INavigationAware
    {
        readonly CompositeDisposable _disposable = new();

        public ReactivePropertySlim<string> AccountText { get; } = new("");
        public ReactivePropertySlim<bool> LoginEnabled { get; } = new();
        public ReactiveCommand LoginCommand { get; } = new();
        public ReactiveCommand BackCommand { get; } = new();

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var isFirstLogin = navigationContext.Parameters["FirstLogin"] as bool? ?? false;

            LoginCommand.Subscribe(() => { })
                .AddTo(_disposable);
            BackCommand.Subscribe(() =>
                    navigationContext.NavigationService.RequestNavigate(nameof(SelectSNS),
                        new NavigationParameters {{"FirstLogin", isFirstLogin}}))
                .AddTo(_disposable);
            AccountText.Subscribe(text => LoginEnabled.Value = IsAccountText(text)).AddTo(_disposable);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _disposable.Clear();
        }

        bool IsAccountText(string accountText)
        {
            return Regex.IsMatch(accountText, @"[^@]+@[\w\-\._]+\.[A-Za-z]+$");
        }
    }
}
