using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using KoharuYomiageApp.Application.ReadText.Interfaces;
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
        readonly UpdateTextListViewPresenter _updateTextListViewPresenter;

        readonly ImageSource KoharuImage0 = new BitmapImage(new Uri("pack://application:,,,/Resources/koharu0.png"));
        readonly ImageSource KoharuImage1 = new BitmapImage(new Uri("pack://application:,,,/Resources/koharu1.png"));

        public MainControlViewModel(UpdateTextListViewPresenter updateTextListViewPresenter,
            ChangeImagePresenter changeImagePresenter)
        {
            _updateTextListViewPresenter = updateTextListViewPresenter;
            _changeImagePresenter = changeImagePresenter;
            KoharuImage.Value = KoharuImage0;
        }

        public ObservableCollection<TextItem> TextList { get; } = new();
        public ReactivePropertySlim<ImageSource> KoharuImage { get; } = new();
        public ReactivePropertySlim<char> VolumeIcon { get; } = new('\uE767');
        public ReactivePropertySlim<double> Volume { get; } = new(0.65);

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _updateTextListViewPresenter.OnDeleteItem
                .Subscribe(item => TextList.Remove(new TextItem(item.Item1, item.Item2)))
                .AddTo(_disposable);
            _updateTextListViewPresenter.OnAddItem
                .Subscribe(item => TextList.Add(new TextItem(item.Item1, item.Item2)))
                .AddTo(_disposable);
            _changeImagePresenter.OnOpenMouth.Subscribe(_ => KoharuImage.Value = KoharuImage1).AddTo(_disposable);
            _changeImagePresenter.OnCloseMouth.Subscribe(_ => KoharuImage.Value = KoharuImage0).AddTo(_disposable);
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
