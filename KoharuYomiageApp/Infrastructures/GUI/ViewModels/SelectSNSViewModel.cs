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
            var isFirstLogin = navigationContext.Parameters["FirstLogin"] as bool? ?? false;
            if (!isFirstLogin)
            {
                BackButtonVisibility.Value = Visibility.Visible;
            }

            SelectMastodonCommand
                .Subscribe(() => navigationContext.NavigationService.RequestNavigate(nameof(MastodonLogin),
                    new NavigationParameters {{"FirstLogin", isFirstLogin}}))
                .AddTo(_disposable);
            BackCommand.Subscribe(() => { })
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
