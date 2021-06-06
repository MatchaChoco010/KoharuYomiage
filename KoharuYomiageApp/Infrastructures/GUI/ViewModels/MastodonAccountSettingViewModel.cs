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
                    case 4:
                        type = MastodonAccountSettingController.MastodonVoiceProfileType.FollowNotification;
                        Title.Value = "フォロー通知";
                        SampleText.Value = @"フォロー通知サンプルさんからフォローされたよ！";
                        break;
                    case 5:
                        type = MastodonAccountSettingController.MastodonVoiceProfileType.FollowRequestNotification;
                        Title.Value = "フォローリクエスト通知";
                        SampleText.Value = @"フォローリクエスト通知サンプルさんからフォローリクエストされたよ！";
                        break;
                    case 6:
                        type = MastodonAccountSettingController.MastodonVoiceProfileType.MentionNotification;
                        Title.Value = "メンションの通知";
                        SampleText.Value = @"メンションの通知サンプルさんからメンションだよ！
投稿サンプルさんの投稿
メンションされた投稿のサンプルです！
画像にDescriptionが付いている場合、それも読み上げます。";
                        break;
                    case 7:
                        type = MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveMentionNotification;
                        Title.Value = "NSFWなメンションの通知";
                        SampleText.Value = @"メンションの通知サンプルさんからメンションだよ！
投稿サンプルさんの投稿
スポイラーテキスト。
メンションされたNSFWな投稿のサンプルです！
画像にDescriptionが付いている場合、それも読み上げます。";
                        break;
                    case 8:
                        type = MastodonAccountSettingController.MastodonVoiceProfileType.ReblogNotification;
                        Title.Value = "ブーストの通知";
                        SampleText.Value = @"ブーストの通知サンプルさんからのブーストだよ！
投稿サンプルさんの投稿
ブーストされた投稿のサンプルです！
画像にDescriptionが付いている場合、それも読み上げます。";
                        break;
                    case 9:
                        type = MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveReblogNotification;
                        Title.Value = "NSFWなブーストの通知";
                        SampleText.Value = @"ブーストの通知サンプルさんからのブーストだよ！
投稿サンプルさんの投稿
スポイラーテキスト。
ブーストされたNSFWな投稿のサンプルです！
画像にDescriptionが付いている場合、それも読み上げます。";
                        break;
                    case 10:
                        type = MastodonAccountSettingController.MastodonVoiceProfileType.FavoriteNotification;
                        Title.Value = "投稿のファボ通知";
                        SampleText.Value = @"投稿のファボ通知サンプルさんからのファボだよ！
投稿サンプルさんの投稿
ファボされた投稿のサンプルです！
画像にDescriptionが付いている場合、それも読み上げます。";
                        break;
                    case 11:
                        type = MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveFavoriteNotification;
                        Title.Value = "NSFWな投稿のファボ通知";
                        SampleText.Value = @"投稿のファボ通知サンプルさんからのファボだよ！
投稿サンプルさんの投稿
スポイラーテキスト。
ファボされたNSFWな投稿のサンプルです！
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

            Volume.SelectMany(_ => SetVoiceProfile(SelectedIndex.Value)).Subscribe().AddTo(_disposable);
            Speed.SelectMany(_ => SetVoiceProfile(SelectedIndex.Value)).Subscribe().AddTo(_disposable);
            Tone.SelectMany(_ => SetVoiceProfile(SelectedIndex.Value)).Subscribe().AddTo(_disposable);
            Alpha.SelectMany(_ => SetVoiceProfile(SelectedIndex.Value)).Subscribe().AddTo(_disposable);
            ToneScale.SelectMany(_ => SetVoiceProfile(SelectedIndex.Value)).Subscribe().AddTo(_disposable);
            ComponentNormal.SelectMany(_ => SetVoiceProfile(SelectedIndex.Value)).Subscribe().AddTo(_disposable);
            ComponentHappy.SelectMany(_ => SetVoiceProfile(SelectedIndex.Value)).Subscribe().AddTo(_disposable);
            ComponentAnger.SelectMany(_ => SetVoiceProfile(SelectedIndex.Value)).Subscribe().AddTo(_disposable);
            ComponentSorrow.SelectMany(_ => SetVoiceProfile(SelectedIndex.Value)).Subscribe().AddTo(_disposable);
            ComponentCalmness.SelectMany(_ => SetVoiceProfile(SelectedIndex.Value)).Subscribe().AddTo(_disposable);

            PlayButtonCommand.Select(_ => Observable.StartAsync(async cancellationToken =>
                {
                    var type = SelectedIndex.Value switch
                    {
                        0 => MastodonAccountSettingController.MastodonVoiceProfileType.Status,
                        1 => MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveStatus,
                        2 => MastodonAccountSettingController.MastodonVoiceProfileType.BoostedStatus,
                        3 => MastodonAccountSettingController.MastodonVoiceProfileType.BoostedSensitiveStatus,
                        4 => MastodonAccountSettingController.MastodonVoiceProfileType.FollowNotification,
                        5 => MastodonAccountSettingController.MastodonVoiceProfileType.FollowRequestNotification,
                        6 => MastodonAccountSettingController.MastodonVoiceProfileType.MentionNotification,
                        7 => MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveMentionNotification,
                        8 => MastodonAccountSettingController.MastodonVoiceProfileType.ReblogNotification,
                        9 => MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveReblogNotification,
                        10 => MastodonAccountSettingController.MastodonVoiceProfileType.FavoriteNotification,
                        11 => MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveFavoriteNotification,
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
                4 => MastodonAccountSettingController.MastodonVoiceProfileType.FollowNotification,
                5 => MastodonAccountSettingController.MastodonVoiceProfileType.FollowRequestNotification,
                6 => MastodonAccountSettingController.MastodonVoiceProfileType.MentionNotification,
                7 => MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveMentionNotification,
                8 => MastodonAccountSettingController.MastodonVoiceProfileType.ReblogNotification,
                9 => MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveReblogNotification,
                10 => MastodonAccountSettingController.MastodonVoiceProfileType.FavoriteNotification,
                11 => MastodonAccountSettingController.MastodonVoiceProfileType.SensitiveFavoriteNotification,
                _ => throw new InvalidProgramException(),
            };
            var data = new MastodonAccountSettingController.MastodonVoiceProfileData(Volume.Value, Speed.Value,
                Tone.Value, Alpha.Value, ToneScale.Value, ComponentNormal.Value, ComponentHappy.Value,
                ComponentAnger.Value, ComponentSorrow.Value, ComponentCalmness.Value);
            await _controller.SetVoiceProfile(Username.Value, Instance.Value, type, data, cancellationToken);
        });
    }
}
