﻿using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using KoharuYomiageApp.Presentation.GUI;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class MainControlViewModel : BindableBase, INavigationAware, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        readonly ImageSource _koharuImage0 = new BitmapImage(new Uri("pack://application:,,,/Resources/koharu0.png"));
        readonly ImageSource _koharuImage1 = new BitmapImage(new Uri("pack://application:,,,/Resources/koharu1.png"));
        readonly MainControlController _mainControlController;

        readonly MainControlPresenter _mainControlPresenter;

        bool _isMute;
        double _prevVolume = 0.65;

        public MainControlViewModel(MainControlPresenter mainControlPresenter,
            MainControlController mainControlController)
        {
            _mainControlPresenter = mainControlPresenter;
            _mainControlController = mainControlController;
            KoharuImage.Value = _koharuImage0;
        }

        public ObservableCollection<TextItem> TextList { get; } = new();
        public ReactiveCommand VolumeButtonCommand { get; } = new();
        public ReactivePropertySlim<ImageSource> KoharuImage { get; } = new();
        public ReactivePropertySlim<char> VolumeIcon { get; } = new('\uE767');
        public ReactivePropertySlim<double> Volume { get; } = new(0.65, ReactivePropertyMode.DistinctUntilChanged);

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _mainControlController.StartReading();
            _mainControlController.StartUpdatingVoiceParameter();

            _mainControlPresenter.OnInitializeGlobalVolumeView.Subscribe(volume =>
            {
                _prevVolume = volume;
                Volume.Value = volume;
            });

            _mainControlPresenter.OnDeleteTextListItem
                .Subscribe(item => TextList.Remove(new TextItem(item.Item1, item.Item2)))
                .AddTo(_disposable);
            _mainControlPresenter.OnAddTextListItem
                .Subscribe(item => TextList.Add(new TextItem(item.Item1, item.Item2)))
                .AddTo(_disposable);
            _mainControlPresenter.OnOpenMouth.Subscribe(_ => KoharuImage.Value = _koharuImage1).AddTo(_disposable);
            _mainControlPresenter.OnCloseMouth.Subscribe(_ => KoharuImage.Value = _koharuImage0).AddTo(_disposable);

            Volume.Subscribe(volume => _mainControlController.UpdateVolume(volume)).AddTo(_disposable);
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

        public void Dispose()
        {
            _disposable.Dispose();
            _mainControlController.Dispose();
            VolumeButtonCommand.Dispose();
            KoharuImage.Dispose();
            VolumeIcon.Dispose();
            Volume.Dispose();
        }
    }
}
