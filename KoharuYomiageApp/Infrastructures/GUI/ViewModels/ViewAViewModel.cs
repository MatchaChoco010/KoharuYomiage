using System;
using System.Reactive.Disposables;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class ViewAViewModel : BindableBase, INavigationAware, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        public ReactiveCommand NavigateCommand { get; } = new();

        public void Dispose()
        {
            _disposable.Dispose();
            NavigateCommand.Dispose();
        }

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
