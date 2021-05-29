using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Windows.Media;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class SettingViewModel : BindableBase, INavigationAware, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        public ReactiveCommand LicenseButtonCommand { get; } = new();
        public ReactiveCommand AccountListButtonCommand { get; } = new();
        public ReactiveCommand BackCommand { get; } = new();
        public ReactivePropertySlim<uint> BufferSize { get; } = new(5);
        public ReactivePropertySlim<Dictionary<string, Brush>> IconBrushes { get; } = new();

        public void Dispose()
        {
            _disposable.Dispose();
            LicenseButtonCommand.Dispose();
            AccountListButtonCommand.Dispose();
            BackCommand.Dispose();
            BufferSize.Dispose();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            BackCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(MainControl)))
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
