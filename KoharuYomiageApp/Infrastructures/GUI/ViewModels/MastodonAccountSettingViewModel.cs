using System;
using System.Reactive.Disposables;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class MastodonAccountSettingViewModel : BindableBase, INavigationAware, IRegionMemberLifetime, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        public ReactiveCommand PlayButtonCommand { get; } = new();
        public ReactiveCommand BackCommand { get; } = new();
        public ReactivePropertySlim<double> Volume { get; } = new(0.5, ReactivePropertyMode.DistinctUntilChanged);
        public ReactivePropertySlim<double> Speed { get; } = new(0.5, ReactivePropertyMode.DistinctUntilChanged);
        public ReactivePropertySlim<double> Tone { get; } = new(0.5, ReactivePropertyMode.DistinctUntilChanged);
        public ReactivePropertySlim<double> Alpha { get; } = new(0.5, ReactivePropertyMode.DistinctUntilChanged);
        public ReactivePropertySlim<double> ToneScale { get; } = new(0.5, ReactivePropertyMode.DistinctUntilChanged);
        public ReactivePropertySlim<double> ComponentNormal { get; } = new(1.0, ReactivePropertyMode.DistinctUntilChanged);
        public ReactivePropertySlim<double> ComponentHappy { get; } = new(0.0, ReactivePropertyMode.DistinctUntilChanged);
        public ReactivePropertySlim<double> ComponentAnger { get; } = new(0.0, ReactivePropertyMode.DistinctUntilChanged);
        public ReactivePropertySlim<double> ComponentSorrow { get; } = new(0.0, ReactivePropertyMode.DistinctUntilChanged);
        public ReactivePropertySlim<double> ComponentCalmness { get; } = new(0.0, ReactivePropertyMode.DistinctUntilChanged);

        public ReactivePropertySlim<int> SelectedIndex { get; } = new();

        public ReactivePropertySlim<string> Username { get; } = new();
        public ReactivePropertySlim<string> Instance { get; } = new();

        public void Dispose()
        {
            _disposable.Dispose();
            BackCommand.Dispose();
            PlayButtonCommand.Dispose();
            Volume.Dispose();
            Speed.Dispose();
            Tone.Dispose();
            Alpha.Dispose();
            ToneScale.Dispose();
            ComponentNormal.Dispose();
            ComponentHappy.Dispose();
            ComponentAnger.Dispose();
            ComponentSorrow.Dispose();
            ComponentCalmness.Dispose();
            SelectedIndex.Dispose();
            Username.Dispose();
            Instance.Dispose();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Username.Value = navigationContext.Parameters["Username"] as string ?? "";
            Instance.Value = navigationContext.Parameters["Instance"] as string ?? "";

            PlayButtonCommand.Subscribe(_ =>
                {
                    //
                })
                .AddTo(_disposable);
            BackCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(AccountList)))
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

        public bool KeepAlive => false;
    }
}
