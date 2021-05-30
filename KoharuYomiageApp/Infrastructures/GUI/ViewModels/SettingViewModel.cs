using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Threading;
using System.Windows.Media;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using KoharuYomiageApp.Presentation.GUI;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class SettingViewModel : BindableBase, INavigationAware, IRegionMemberLifetime, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        readonly SettingController _settingController;

        CancellationTokenSource? _cancellationTokenSource;

        public SettingViewModel(SettingController settingController)
        {
            _settingController = settingController;
        }

        public ReactiveCommand LicenseButtonCommand { get; } = new();
        public ReactiveCommand AccountListButtonCommand { get; } = new();
        public ReactiveCommand BackCommand { get; } = new();
        public ReactivePropertySlim<int> BufferSize { get; } = new(-1, ReactivePropertyMode.DistinctUntilChanged);
        public ReactivePropertySlim<Dictionary<string, Brush>> IconBrushes { get; } = new();

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel(true);
            _cancellationTokenSource?.Dispose();
            _disposable.Dispose();
            LicenseButtonCommand.Dispose();
            AccountListButtonCommand.Dispose();
            BackCommand.Dispose();
            BufferSize.Dispose();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            _ = _settingController.GetReadingTextContainerSize(_cancellationTokenSource.Token)
                .ContinueWith(t => BufferSize.Value = t.Result, _cancellationTokenSource.Token);

            BackCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(MainControl)))
                .AddTo(_disposable);
            BufferSize.Subscribe(v =>
            {
                if (v <= 0)
                {
                    BufferSize.Value = 1;
                }
                else
                {
                    _ = _settingController.ChangeReadingTextContainerSize(v, _cancellationTokenSource.Token);
                }
            }).AddTo(_disposable);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _cancellationTokenSource?.Cancel(true);
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            _disposable.Clear();
        }

        public bool KeepAlive => false;
    }
}
