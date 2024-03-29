﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using KoharuYomiageApp.Presentation.GUI;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class MainControlViewModel : BindableBase, INavigationAware, IRegionMemberLifetime, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        readonly ImageSource _koharuImage0 = new BitmapImage(new Uri("pack://application:,,,/Resources/koharu0.png"));
        readonly ImageSource _koharuImage1 = new BitmapImage(new Uri("pack://application:,,,/Resources/koharu1.png"));
        readonly MainControlController _mainControlController;

        readonly MainControlPresenter _mainControlPresenter;

        CancellationTokenSource? _cancellationTokenSource;
        bool _isMute;
        double _prevVolume = 0.65;

        public MainControlViewModel(MainControlPresenter mainControlPresenter,
            MainControlController mainControlController)
        {
            _mainControlPresenter = mainControlPresenter;
            _mainControlController = mainControlController;
            KoharuImage.Value = _koharuImage0;
        }

        public ReactivePropertySlim<List<TextItem>> TextList { get; } = new();
        public ReactiveCommand VolumeButtonCommand { get; } = new();
        public ReactivePropertySlim<ImageSource> KoharuImage { get; } = new();
        public ReactivePropertySlim<char> VolumeIcon { get; } = new('\uE767');
        public ReactivePropertySlim<double> Volume { get; } = new(0.65, ReactivePropertyMode.DistinctUntilChanged);
        public ReactiveCommand SettingButtonCommand { get; } = new();

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel(true);
            _cancellationTokenSource?.Dispose();
            _disposable.Dispose();
            VolumeButtonCommand.Dispose();
            KoharuImage.Dispose();
            VolumeIcon.Dispose();
            Volume.Dispose();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _ = _mainControlController.StartReading(_cancellationTokenSource.Token);
            _ = _mainControlController.GetVolume(_cancellationTokenSource.Token)
                .ContinueWith(t => Volume.Value = t.Result, _cancellationTokenSource.Token);

            _mainControlPresenter.TextListAsObservable.Subscribe(list =>
                TextList.Value = list.Select(item => new TextItem(item.Item1, item.Item2)).ToList()).AddTo(_disposable);

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
            SettingButtonCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(Setting)))
                .AddTo(_disposable);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _cancellationTokenSource?.Cancel(true);
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            _disposable.Clear();
        }

        public bool KeepAlive => false;

        public record TextItem(Guid Id, string Text);
    }
}
