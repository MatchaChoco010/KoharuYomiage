using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ImTools;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using KoharuYomiageApp.Presentation.GUI;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class MastodonAccountSettingViewModel : BindableBase, INavigationAware, IRegionMemberLifetime, IDisposable
    {
        readonly CompositeDisposable _disposable = new();
        readonly MastodonAccountSettingController _controller;

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
        public ReactivePropertySlim<string> Title { get; } = new();
        public ReactivePropertySlim<string> SampleText { get; } = new();

        public MastodonAccountSettingViewModel(MastodonAccountSettingController controller)
        {
            _controller = controller;
        }

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

            SelectedIndex.Select(i => Observable.StartAsync(async cancellationToken =>
            {
                MastodonAccountSettingController.MastodonVoiceProfileType type;
                switch (i)
                {
                    case 0:
                        type = MastodonAccountSettingController.MastodonVoiceProfileType.Status;
                        Title.Value = "投稿";
                        SampleText.Value = @"投稿サンプルさんの投稿
投稿のサンプルです！
画像にDescriptionが付いている場合、それも読み上げます。";
                        break;
                    case 1:
                        type = MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveStatus;
                        Title.Value = "NSFWな投稿";
                        SampleText.Value = @"投稿サンプルさんの投稿
スポイラーテキスト。
NSFWな投稿のサンプルです！
画像にDescriptionが付いている場合、それも読み上げます。";
                        break;
                    case 2:
                        type = MastodonAccountSettingController.MastodonVoiceProfileType.BoostedStatus;
                        Title.Value = "Boostされた投稿";
                        SampleText.Value = @"Boost投稿サンプルさんがブースト
投稿サンプルさんの投稿
ブーストされた投稿のサンプルです！
画像にDescriptionが付いている場合、それも読み上げます。";
                        break;
                    case 3:
                        type = MastodonAccountSettingController.MastodonVoiceProfileType.BoostedSensitiveStatus;
                        Title.Value = "NSFWなBoostされた投稿";
                        SampleText.Value = @"Boost投稿サンプルさんがブースト
投稿サンプルさんの投稿
スポイラーテキスト。
ブーストされたNSFWな投稿のサンプルです！
画像にDescriptionが付いている場合、それも読み上げます。";
                        break;
                    default:
                        throw new InvalidProgramException();
                }
                var data = await _controller.GetVoiceProfile(Username.Value, Instance.Value, type, cancellationToken);
                Volume.Value = data.Volume;
                Speed.Value = data.Speed;
                Tone.Value = data.Tone;
                Alpha.Value = data.Alpha;
                ToneScale.Value = data.ToneScale;
                ComponentNormal.Value = data.ComponentNormal;
                ComponentHappy.Value = data.ComponentHappy;
                ComponentAnger.Value = data.ComponentAnger;
                ComponentSorrow.Value = data.ComponentSorrow;
                ComponentCalmness.Value = data.ComponentCalmness;
            })).Switch().Subscribe().AddTo(_disposable);

            Volume.Select(_ => SetVoiceProfile(SelectedIndex.Value)).Switch().Subscribe().AddTo(_disposable);
            Speed.Select(_ => SetVoiceProfile(SelectedIndex.Value)).Switch().Subscribe().AddTo(_disposable);
            Tone.Select(_ => SetVoiceProfile(SelectedIndex.Value)).Switch().Subscribe().AddTo(_disposable);
            Alpha.Select(_ => SetVoiceProfile(SelectedIndex.Value)).Switch().Subscribe().AddTo(_disposable);
            ToneScale.Select(_ => SetVoiceProfile(SelectedIndex.Value)).Switch().Subscribe().AddTo(_disposable);
            ComponentNormal.Select(_ => SetVoiceProfile(SelectedIndex.Value)).Switch().Subscribe().AddTo(_disposable);
            ComponentHappy.Select(_ => SetVoiceProfile(SelectedIndex.Value)).Switch().Subscribe().AddTo(_disposable);
            ComponentAnger.Select(_ => SetVoiceProfile(SelectedIndex.Value)).Switch().Subscribe().AddTo(_disposable);
            ComponentSorrow.Select(_ => SetVoiceProfile(SelectedIndex.Value)).Switch().Subscribe().AddTo(_disposable);
            ComponentCalmness.Select(_ => SetVoiceProfile(SelectedIndex.Value)).Switch().Subscribe().AddTo(_disposable);

            PlayButtonCommand.Select(_ => Observable.StartAsync(async cancellationToken =>
                {
                    var type = SelectedIndex.Value switch
                    {
                        0 => MastodonAccountSettingController.MastodonVoiceProfileType.Status,
                        1 => MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveStatus,
                        2 => MastodonAccountSettingController.MastodonVoiceProfileType.BoostedStatus,
                        3 => MastodonAccountSettingController.MastodonVoiceProfileType.BoostedSensitiveStatus,
                        _ => throw new InvalidProgramException(),
                    };
                    await _controller.PlaySampleVoice(Username.Value, Instance.Value, type, SampleText.Value,
                        cancellationToken);
                }))
                .Switch()
                .Subscribe()
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

        IObservable<System.Reactive.Unit> SetVoiceProfile(int index) => Observable.StartAsync(async cancellationToken =>
        {
            var type = index switch
            {
                0 => MastodonAccountSettingController.MastodonVoiceProfileType.Status,
                1 => MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveStatus,
                2 => MastodonAccountSettingController.MastodonVoiceProfileType.BoostedStatus,
                3 => MastodonAccountSettingController.MastodonVoiceProfileType.BoostedSensitiveStatus,
                _ => throw new InvalidProgramException(),
            };
            var data = new MastodonAccountSettingController.MastodonVoiceProfileData(Volume.Value, Speed.Value,
                Tone.Value, Alpha.Value, ToneScale.Value, ComponentNormal.Value, ComponentHappy.Value,
                ComponentAnger.Value, ComponentSorrow.Value, ComponentCalmness.Value);
            await _controller.SetVoiceProfile(Username.Value, Instance.Value, type, data, cancellationToken);
        });
    }
}
