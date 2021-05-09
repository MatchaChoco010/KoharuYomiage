using System;
using System.Reactive.Disposables;
using System.Windows.Media;
using KoharuYomiageApp.Application.LoadTalker.Interfaces;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using SourceChord.FluentWPF;

namespace KoharuYomiageApp.Application.ViewModels
{
    public class StartViewModel : BindableBase, INavigationAware
    {
        readonly LoadTalkerController _controller;
        readonly CompositeDisposable _disposable = new();
        readonly LoadTalkerPresenter _presenter;
        readonly IDialogService _dialogService;

        public StartViewModel(LoadTalkerController controller, LoadTalkerPresenter presenter, IDialogService dialogService)
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
            StartLoading();
            LoadedCommand.Subscribe(_ => _controller.WindowLoaded()).AddTo(_disposable);
            NavigateCommand.Subscribe(_ => ShowLoadErrorDialogs()).AddTo(_disposable);
            _presenter.OnLoadedTalker.Subscribe(_ => FinishLoading()).AddTo(_disposable);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _disposable.Clear();
        }

        void StartLoading()
        {
            StatusText.Value = "CeVIO AI に接続しています...";
            StartButtonIsEnabled.Value = false;
            StartButtonForeground.Value = Brushes.Black;
            StartButtonBackground.Value = Brushes.Gray;
        }

        void FinishLoading()
        {
            StatusText.Value = "CeVIO AI との接続が完了しました！";
            StartButtonIsEnabled.Value = true;
            StartButtonForeground.Value = Brushes.White;
            StartButtonBackground.Value = AccentColors.ImmersiveSystemAccentBrush;
        }

        void ShowLoadErrorDialogs()
        {
            _dialogService.ShowDialog("LoadTalkerErrorDialogContent");
            _dialogService.ShowDialog("LoadTalkerLinkDialogContent");
            Close.Value = true;
        }
    }
}
