using System;
using System.Reactive.Disposables;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class MainWindowViewModel : BindableBase, IDestructible, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        public MainWindowViewModel(IRegionManager regionManager)
        {
            LoadedCommand.Subscribe(() => regionManager.RequestNavigate("ContentRegion", nameof(Start)))
                .AddTo(_disposable);
        }

        public ReactiveCommand LoadedCommand { get; } = new();

        public void Destroy()
        {
            _disposable.Dispose();
        }

        public void Dispose()
        {
            _disposable.Dispose();
            LoadedCommand.Dispose();
        }
    }
}
