using System.Reactive.Disposables;
using System.Windows;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class SelectSNSViewModel : BindableBase, INavigationAware
    {
        readonly CompositeDisposable _disposable = new();

        public ReactivePropertySlim<Visibility> BackButtonVisibility { get; } = new(Visibility.Hidden);
        public ReactiveCommand SelectMastodonCommand { get; } = new();
        public ReactiveCommand BackCommand { get; } = new();

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            SelectMastodonCommand.Subscribe(() => BackButtonVisibility.Value = Visibility.Visible)
                .AddTo(_disposable);
            BackCommand.Subscribe(() => navigationContext.NavigationService.RequestNavigate(nameof(ViewA)))
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
