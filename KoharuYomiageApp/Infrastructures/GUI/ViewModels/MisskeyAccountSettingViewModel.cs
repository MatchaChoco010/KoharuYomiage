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
    public class MisskeyAccountSettingViewModel : BindableBase, INavigationAware, IRegionMemberLifetime, IDisposable
    {
        readonly CompositeDisposable _disposable = new();
        readonly MisskeyAccountSettingController _controller;

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

        public MisskeyAccountSettingViewModel(MisskeyAccountSettingController controller)
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
                MisskeyAccountSettingController.MisskeyVoiceProfileType type;
                switch (i)
                {
                    case 0:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.Note;
                        Title.Value = "Note";
                        SampleText.Value = @"Noteサンプルさんの投稿
Noteのサンプルです！";
                        break;
                    case 1:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveNote;
                        Title.Value = "NSFWなNote";
                        SampleText.Value = @"NSFWなNoteサンプルさんの投稿
スポイラーテキスト。
NSFWなNoteのサンプルです！";
                        break;
                    case 2:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.Renote;
                        Title.Value = "Renote";
                        SampleText.Value = @"リノートサンプルさんのリノート！
リノートサンプルさんの投稿
リノートのサンプルです！";
                        break;
                    case 3:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveRenote;
                        Title.Value = "NSFWなRenote";
                        SampleText.Value = @"リノートサンプルさんのリノート！
NSFWなリノートサンプルさんの投稿
スポイラーテキスト。
リノートされたNSFWな投稿のサンプルです！";
                        break;
                    case 4:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.ReactionNotification;
                        Title.Value = "Reaction通知";
                        SampleText.Value = @"リアクション通知サンプルさんが:reaction:でリアクションしたよ！
リアクション通知サンプルさんの投稿
リアクション通知サンプル投稿です！";
                        break;
                    case 5:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveReactionNotification;
                        Title.Value = "NSFWなNoteへのReaction通知";
                        SampleText.Value = @"リアクション通知サンプルさんが:reaction:でリアクションしたよ！
リアクション通知サンプルさんの投稿
スポイラーテキスト。
リアクション通知サンプル投稿です！";
                        break;
                    case 6:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.ReplyNotification;
                        Title.Value = "Reply通知";
                        SampleText.Value = @"リプライ通知サンプルさんにリプライされたよ！
リプライサンプルです！";
                        break;
                    case 7:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveReplyNotification;
                        Title.Value = "NSFWなReply通知";
                        SampleText.Value = @"リプライ通知サンプルさんにリプライされたよ！
スポイラーテキスト。
リプライサンプルです！";
                        break;
                    case 8:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.RenoteNotification;
                        Title.Value = "Renote通知";
                        SampleText.Value = @"リノート通知サンプルさんにリノートされたよ！
リノート通知サンプルさんの投稿
リノート通知のサンプルです！";
                        break;
                    case 9:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveRenoteNotification;
                        Title.Value = "NSFWなRenote通知";
                        SampleText.Value = @"リノート通知サンプルさんにリノートされたよ！
リノート通知サンプルさんの投稿
スポイラーテキスト。
リノート通知のサンプルです！";
                        break;
                    case 10:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.QuoteNotification;
                        Title.Value = "引用通知";
                        SampleText.Value = @"引用通知サンプルさんに引用リノートされたよ！
引用通知サンプルさんの投稿
引用通知のサンプルです！";
                        break;
                    case 11:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveQuoteNotification;
                        Title.Value = "NSFWな引用通知";
                        SampleText.Value = @"引用通知サンプルさんに引用リノートされたよ！
引用通知サンプルさんの投稿
スポイラーテキスト。
引用通知のサンプルです！";
                        break;
                    case 12:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.MentionNotification;
                        Title.Value = "Mention通知";
                        SampleText.Value = @"メンション通知サンプルさんにメンションされたよ！
メンション通知のサンプルです！";
                        break;
                    case 13:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveMentionNotification;
                        Title.Value = "NSFWなMention通知";
                        SampleText.Value = @"メンション通知サンプルさんにメンションされたよ！
スポイラーテキスト。
メンション通知のサンプルです！";
                        break;
                    case 14:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.FollowNotification;
                        Title.Value = "Follow通知";
                        SampleText.Value = @"フォロー通知サンプルさんにフォローされたよ！";
                        break;
                    case 15:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.FollowRequestAcceptedNotification;
                        Title.Value = "Follow承認通知";
                        SampleText.Value = @"フォロー承認通知サンプルさんにフォローリクエストが承認されたよ！";
                        break;
                    case 16:
                        type = MisskeyAccountSettingController.MisskeyVoiceProfileType.ReceiveFollowRequestNotification;
                        Title.Value = "Follow Request受け取り通知";
                        SampleText.Value = @"フォローリクエスト受取通知サンプルさんからフォローリクエストされたよ！";
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
                        0 => MisskeyAccountSettingController.MisskeyVoiceProfileType.Note,
                        1 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveNote,
                        2 => MisskeyAccountSettingController.MisskeyVoiceProfileType.Renote,
                        3 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveRenote,
                        4 => MisskeyAccountSettingController.MisskeyVoiceProfileType.ReactionNotification,
                        5 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveReactionNotification,
                        6 => MisskeyAccountSettingController.MisskeyVoiceProfileType.ReplyNotification,
                        7 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveReplyNotification,
                        8 => MisskeyAccountSettingController.MisskeyVoiceProfileType.RenoteNotification,
                        9 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveRenoteNotification,
                        10 => MisskeyAccountSettingController.MisskeyVoiceProfileType.QuoteNotification,
                        11 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveQuoteNotification,
                        12 => MisskeyAccountSettingController.MisskeyVoiceProfileType.MentionNotification,
                        13 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveMentionNotification,
                        14 => MisskeyAccountSettingController.MisskeyVoiceProfileType.FollowNotification,
                        15 => MisskeyAccountSettingController.MisskeyVoiceProfileType.FollowRequestAcceptedNotification,
                        16 => MisskeyAccountSettingController.MisskeyVoiceProfileType.ReceiveFollowRequestNotification,
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
                0 => MisskeyAccountSettingController.MisskeyVoiceProfileType.Note,
                1 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveNote,
                2 => MisskeyAccountSettingController.MisskeyVoiceProfileType.Renote,
                3 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveRenote,
                4 => MisskeyAccountSettingController.MisskeyVoiceProfileType.ReactionNotification,
                5 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveReactionNotification,
                6 => MisskeyAccountSettingController.MisskeyVoiceProfileType.ReplyNotification,
                7 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveReplyNotification,
                8 => MisskeyAccountSettingController.MisskeyVoiceProfileType.RenoteNotification,
                9 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveRenoteNotification,
                10 => MisskeyAccountSettingController.MisskeyVoiceProfileType.QuoteNotification,
                11 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveQuoteNotification,
                12 => MisskeyAccountSettingController.MisskeyVoiceProfileType.MentionNotification,
                13 => MisskeyAccountSettingController.MisskeyVoiceProfileType.SensitiveMentionNotification,
                14 => MisskeyAccountSettingController.MisskeyVoiceProfileType.FollowNotification,
                15 => MisskeyAccountSettingController.MisskeyVoiceProfileType.FollowRequestAcceptedNotification,
                16 => MisskeyAccountSettingController.MisskeyVoiceProfileType.ReceiveFollowRequestNotification,
                _ => throw new InvalidProgramException(),
            };
            var data = new MisskeyAccountSettingController.MisskeyVoiceProfileData(Volume.Value, Speed.Value,
                Tone.Value, Alpha.Value, ToneScale.Value, ComponentNormal.Value, ComponentHappy.Value,
                ComponentAnger.Value, ComponentSorrow.Value, ComponentCalmness.Value);
            await _controller.SetVoiceProfile(Username.Value, Instance.Value, type, data, cancellationToken);
        });
    }
}
