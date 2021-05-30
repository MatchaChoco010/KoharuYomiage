using System;
using System.Reactive.Disposables;
using System.Windows.Media;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using KoharuYomiageApp.Infrastructures.GUI.Views.Dialogs;
using KoharuYomiageApp.Presentation.GUI;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using SourceChord.FluentWPF;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class StartViewModel : BindableBase, INavigationAware, IDisposable
    {
        readonly IDialogService _dialogService;
        readonly CompositeDisposable _disposable = new();
        readonly StartController _startController;
        readonly StartPresenter _startPresenter;

        public StartViewModel(StartPresenter startPresenter, StartController startController,
            IDialogService dialogService)
        {
            _startPresenter = startPresenter;
            _startController = startController;
            _dialogService = dialogService;
        }

        public ReactiveCommand LoadedCommand { get; } = new();
        public ReactiveCommand NavigateCommand { get; } = new();
        public ReactivePropertySlim<bool> Close { get; } = new();
        public ReactivePropertySlim<string> StatusText { get; } = new("CeVIO AI と接続しています……");
        public ReactivePropertySlim<bool> StartButtonIsEnabled { get; } = new();
        public ReactivePropertySlim<Brush> StartButtonForeground { get; } = new(Brushes.Black);
        public ReactivePropertySlim<Brush> StartButtonBackground { get; } = new(Brushes.Gray);

        public void Dispose()
        {
            _disposable.Dispose();
            LoadedCommand.Dispose();
            NavigateCommand.Dispose();
            Close.Dispose();
            StatusText.Dispose();
            StartButtonIsEnabled.Dispose();
            StartButtonForeground.Dispose();
            StartButtonBackground.Dispose();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadedCommand.Subscribe(_ =>
            {
                _startController.InitializeReadingTextContainerSize();
                _startController.WindowLoaded();
            }).AddTo(_disposable);
            NavigateCommand.Subscribe(_ =>
            {
                _startController.StartUpdatingTextList();
                _startController.StartUpdatingVoiceParameter();
                _startController.PushStartButton();
            }).AddTo(_disposable);

            _startPresenter.OnFinishLoadTalker.Subscribe(_ =>
                {
                    StartButtonIsEnabled.Value = true;
                    StatusText.Value = "CeVIO AI と接続が完了しました！";
                    StartButtonForeground.Value = Brushes.White;
                    StartButtonBackground.Value = AccentColors.ImmersiveSystemAccentBrush;
                })
                .AddTo(_disposable);
            _startPresenter.OnShowLoadTalkerError.Subscribe(_ => ShowLoadErrorDialogs())
                .AddTo(_disposable);
            _startPresenter.OnStartRegisterAccount.Subscribe(_ =>
                navigationContext.NavigationService.RequestNavigate(nameof(SelectSNS),
                    new NavigationParameters {{"FirstLogin", true}})).AddTo(_disposable);
            _startPresenter.OnStartApp
                .Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(MainControl)))
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

        void ShowLoadErrorDialogs()
        {
            _dialogService.ShowDialog(nameof(LoadTalkerError));
            _dialogService.ShowDialog(nameof(LoadTalkerLink));
            Close.Value = true;
        }
    }
}
