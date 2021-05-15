using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using KoharuYomiageApp.Application.ReadText.Interfaces;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class MainControlViewModel : BindableBase, INavigationAware
    {
        readonly CompositeDisposable _disposable = new();
        readonly UpdateTextListViewPresenter _updateTextListViewPresenter;

        public MainControlViewModel(UpdateTextListViewPresenter updateTextListViewPresenter)
        {
            _updateTextListViewPresenter = updateTextListViewPresenter;
        }

        public ObservableCollection<TextItem> TextList { get; } = new();
        public ReactivePropertySlim<char> VolumeIcon { get; } = new('\uE767');
        public ReactivePropertySlim<double> Volume { get; } = new(1.0);

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _updateTextListViewPresenter.OnDeleteItem
                .Subscribe(item => TextList.Remove(new TextItem(item.Item1, item.Item2)))
                .AddTo(_disposable);
            _updateTextListViewPresenter.OnAddItem
                .Subscribe(item => TextList.Add(new TextItem(item.Item1, item.Item2)))
                .AddTo(_disposable);
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
