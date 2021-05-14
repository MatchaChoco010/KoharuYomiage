using System;
using System.Reactive.Disposables;
using System.Windows.Media;
using KoharuYomiageApp.Application.LoadTalker.Interfaces;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class StartViewModel : BindableBase, INavigationAware
    {
        readonly LoadTalkerController _controller;
        readonly IDialogService _dialogService;
        readonly CompositeDisposable _disposable = new();
        readonly LoadTalkerPresenter _presenter;

        public StartViewModel(LoadTalkerController controller, LoadTalkerPresenter presenter,
            IDialogService dialogService)
        {
            _controller = controller;
            _presenter = presenter;
            _dialogService = dialogService;
        }

        public ReactiveCommand LoadedCommand { get; } = new();
        public ReactiveCommand NavigateCommand { get; } = new();
        public ReactivePropertySlim<bool> Close { get; } = new();
        public ReactivePropertySlim<string> StatusText { get; } = new();
        public ReactivePropertySlim<bool> StartButtonIsEnabled { get; } = new();
        public ReactivePropertySlim<Brush> StartButtonForeground { get; } = new();
        public ReactivePropertySlim<Brush> StartButtonBackground { get; } = new();

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadedCommand.Subscribe(_ => _controller.WindowLoaded()).AddTo(_disposable);
            NavigateCommand.Subscribe(_ =>
                    navigationContext.NavigationService.RequestNavigate(nameof(SelectSNS),
                        new NavigationParameters {{"FirstLogin", true}}))
                .AddTo(_disposable);

            _presenter.StatusText.Subscribe(statusText => StatusText.Value = statusText).AddTo(_disposable);
            _presenter.StartButtonIsEnabled.Subscribe(isEnabled => StartButtonIsEnabled.Value = isEnabled)
                .AddTo(_disposable);
            _presenter.StartButtonForeground.Subscribe(brush => StartButtonForeground.Value = brush).AddTo(_disposable);
            _presenter.StartButtonBackground.Subscribe(brush => StartButtonBackground.Value = brush).AddTo(_disposable);
            _presenter.OnFailureLoadTalker.Subscribe(_ => ShowLoadErrorDialogs()).AddTo(_disposable);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _disposable.Clear();
        }

        void ShowLoadErrorDialogs()
        {
            _dialogService.ShowDialog(nameof(LoadTalkerErrorDialogContent));
            _dialogService.ShowDialog(nameof(LoadTalkerLinkDialogContent));
            Close.Value = true;
        }
    }
}
