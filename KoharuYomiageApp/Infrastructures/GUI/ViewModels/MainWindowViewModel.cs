using System.Reactive.Disposables;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class MainWindowViewModel : BindableBase, IDestructible
    {
        readonly IRegionManager _regionManager;
        readonly CompositeDisposable disposable = new();

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LoadedCommand.Subscribe(() => _regionManager.RequestNavigate("ContentRegion", nameof(Start)))
                .AddTo(disposable);
        }

        public ReactiveCommand LoadedCommand { get; } = new();

        public void Destroy()
        {
            disposable.Dispose();
        }
    }
}
