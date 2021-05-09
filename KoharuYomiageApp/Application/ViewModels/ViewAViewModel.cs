using System.Reactive.Disposables;
using KoharuYomiageApp.Infrastructures.Views;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Application.ViewModels
{
    public class ViewAViewModel : BindableBase, INavigationAware
    {
        readonly CompositeDisposable _disposable = new();

        public ReactiveCommand NavigateCommand { get; } = new();

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            NavigateCommand.Subscribe(() => navigationContext.NavigationService.RequestNavigate(nameof(ViewB)))
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
