using System;
using System.Reactive.Disposables;
using System.Windows.Media;
using KoharuYomiageApp.Application.WindowLoaded.Interfaces;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using KoharuYomiageApp.Infrastructures.GUI.Views.Dialogs;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using SourceChord.FluentWPF;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class StartViewModel : BindableBase, INavigationAware
    {
        readonly IDialogService _dialogService;
        readonly CompositeDisposable _disposable = new();
        readonly FinishLoadTalkerPresenter _finishLoadTalkerPresenter;
        readonly PushStartButtonController _pushStartButtonController;
        readonly ShowLoadTalkerErrorPresenter _showLoadTalkerErrorPresenter;
        readonly StartAppPresenter _startAppPresenter;
        readonly StartRegisteringAccountPresenter _startRegisteringAccountPresenter;
        readonly WindowLoadedController _windowLoadedController;

        public StartViewModel(WindowLoadedController windowLoadedController,
            FinishLoadTalkerPresenter finishLoadTalkerPresenter,
            ShowLoadTalkerErrorPresenter showLoadTalkerErrorPresenter,
            PushStartButtonController pushStartButtonController,
            StartRegisteringAccountPresenter startRegisteringAccountPresenter, StartAppPresenter startAppPresenter,
            IDialogService dialogService)
        {
            _windowLoadedController = windowLoadedController;
            _finishLoadTalkerPresenter = finishLoadTalkerPresenter;
            _showLoadTalkerErrorPresenter = showLoadTalkerErrorPresenter;
            _pushStartButtonController = pushStartButtonController;
            _startRegisteringAccountPresenter = startRegisteringAccountPresenter;
            _startAppPresenter = startAppPresenter;
            _dialogService = dialogService;
        }

        public ReactiveCommand LoadedCommand { get; } = new();
        public ReactiveCommand NavigateCommand { get; } = new();
        public ReactivePropertySlim<bool> Close { get; } = new();
        public ReactivePropertySlim<string> StatusText { get; } = new("CeVIO AI と接続しています……");
        public ReactivePropertySlim<bool> StartButtonIsEnabled { get; } = new();
        public ReactivePropertySlim<Brush> StartButtonForeground { get; } = new(Brushes.Black);
        public ReactivePropertySlim<Brush> StartButtonBackground { get; } = new(Brushes.Gray);

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadedCommand.Subscribe(_ => _windowLoadedController.WindowLoaded()).AddTo(_disposable);
            NavigateCommand.Subscribe(_ => _pushStartButtonController.PushStartButton()).AddTo(_disposable);

            _finishLoadTalkerPresenter.OnFinishLoadTalker.Subscribe(_ =>
                {
                    StartButtonIsEnabled.Value = true;
                    StatusText.Value = "CeVIO AI と接続が完了しました！";
                    StartButtonForeground.Value = Brushes.White;
                    StartButtonBackground.Value = AccentColors.ImmersiveSystemAccentBrush;
                })
                .AddTo(_disposable);
            _showLoadTalkerErrorPresenter.OnShowLoadTalkerError.Subscribe(_ => ShowLoadErrorDialogs())
                .AddTo(_disposable);
            _startRegisteringAccountPresenter.OnStartRegisterAccount.Subscribe(_ =>
                {
                    navigationContext.NavigationService.RequestNavigate(nameof(SelectSNS),
                        new NavigationParameters {{"FirstLogin", true}});
                }
            ).AddTo(_disposable);
            _startAppPresenter.OnStartApp
                .Subscribe(_ =>
                {
                    navigationContext.NavigationService.RequestNavigate(nameof(MainControl));
                }).AddTo(_disposable);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _disposable.Clear();
        }

        void ShowLoadErrorDialogs()
        {
            _dialogService.ShowDialog(nameof(LoadTalkerError));
            _dialogService.ShowDialog(nameof(LoadTalkerLink));
            Close.Value = true;
        }
    }
}
