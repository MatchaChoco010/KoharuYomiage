﻿using System;
using System.Reactive.Disposables;
using System.Windows;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class SelectSNSViewModel : BindableBase, INavigationAware, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        public ReactivePropertySlim<Visibility> BackButtonVisibility { get; } = new(Visibility.Hidden);
        public ReactiveCommand SelectMastodonCommand { get; } = new();
        public ReactiveCommand SelectMisskeyCommand { get; } = new();
        public ReactiveCommand BackCommand { get; } = new();

        public void Dispose()
        {
            _disposable.Dispose();
            BackButtonVisibility.Dispose();
            SelectMastodonCommand.Dispose();
            SelectMisskeyCommand.Dispose();
            BackCommand.Dispose();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var isFirstLogin = navigationContext.Parameters["FirstLogin"] as bool? ?? false;
            if (!isFirstLogin)
            {
                BackButtonVisibility.Value = Visibility.Visible;
            }

            SelectMastodonCommand
                .Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(MastodonLogin),
                    new NavigationParameters { { "FirstLogin", isFirstLogin } }))
                .AddTo(_disposable);
            SelectMisskeyCommand
                .Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(MisskeyLogin),
                    new NavigationParameters { { "FirstLogin", isFirstLogin } }))
                .AddTo(_disposable);
            BackCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(AccountList)))
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
