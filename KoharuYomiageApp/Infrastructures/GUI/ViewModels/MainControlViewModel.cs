using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using KoharuYomiageApp.Application.ReadText.Interfaces;
using KoharuYomiageApp.Application.UpdateVoiceParameters.Interfaces;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class MainControlViewModel : BindableBase, INavigationAware
    {
        readonly ChangeImagePresenter _changeImagePresenter;
        readonly CompositeDisposable _disposable = new();

        readonly ImageSource _koharuImage0 = new BitmapImage(new Uri("pack://application:,,,/Resources/koharu0.png"));
        readonly ImageSource _koharuImage1 = new BitmapImage(new Uri("pack://application:,,,/Resources/koharu1.png"));
        readonly StartReadingController _startReadingController;
        readonly StartUpdatingVoiceParameterController _startUpdatingVoiceParameterController;
        readonly UpdateGlobalVolumeController _updateGlobalVolumeController;
        readonly UpdateTextListViewPresenter _updateTextListViewPresenter;
        bool _isMute;

        double _prevVolume = 0.65;

        public MainControlViewModel(UpdateTextListViewPresenter updateTextListViewPresenter,
            ChangeImagePresenter changeImagePresenter, StartReadingController startReadingController,
            StartUpdatingVoiceParameterController startUpdatingVoiceParameterController,
            UpdateGlobalVolumeController updateGlobalVolumeController)
        {
            _updateTextListViewPresenter = updateTextListViewPresenter;
            _changeImagePresenter = changeImagePresenter;
            _startReadingController = startReadingController;
            _startUpdatingVoiceParameterController = startUpdatingVoiceParameterController;
            _updateGlobalVolumeController = updateGlobalVolumeController;
            KoharuImage.Value = _koharuImage0;
        }

        public ObservableCollection<TextItem> TextList { get; } = new();
        public ReactiveCommand VolumeButtonCommand { get; } = new();
        public ReactivePropertySlim<ImageSource> KoharuImage { get; } = new();
        public ReactivePropertySlim<char> VolumeIcon { get; } = new('\uE767');
        public ReactivePropertySlim<double> Volume { get; } = new(0.65);

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _startReadingController.StartReading();
            _startUpdatingVoiceParameterController.Start();

            _updateTextListViewPresenter.OnDeleteItem
                .Subscribe(item => TextList.Remove(new TextItem(item.Item1, item.Item2)))
                .AddTo(_disposable);
            _updateTextListViewPresenter.OnAddItem
                .Subscribe(item => TextList.Add(new TextItem(item.Item1, item.Item2)))
                .AddTo(_disposable);
            _changeImagePresenter.OnOpenMouth.Subscribe(_ => KoharuImage.Value = _koharuImage1).AddTo(_disposable);
            _changeImagePresenter.OnCloseMouth.Subscribe(_ => KoharuImage.Value = _koharuImage0).AddTo(_disposable);

            Volume.Subscribe(volume => _updateGlobalVolumeController.Update(volume)).AddTo(_disposable);
            VolumeButtonCommand.Subscribe(_ =>
            {
                _isMute = !_isMute;
                if (_isMute)
                {
                    VolumeIcon.Value = '\uE74F';
                    _prevVolume = Volume.Value;
                    Volume.Value = 0.0;
                }
                else
                {
                    VolumeIcon.Value = '\uE767';
                    Volume.Value = _prevVolume;
                }
            }).AddTo(_disposable);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _disposable.Clear();
        }

        public record TextItem(Guid Id, string Text);
    }
}
