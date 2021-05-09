using System;
using System.Reactive;
using System.Reactive.Subjects;
using System.Windows.Media;
using KoharuYomiageApp.Application.LoadTalker.UseCases;
using Reactive.Bindings;
using SourceChord.FluentWPF;

namespace KoharuYomiageApp.Application.LoadTalker.Interfaces
{
    public class LoadTalkerPresenter : ILoadTalkerOutputBoundary
    {
        readonly Subject<Unit> onLoadedWindow = new();
        readonly Subject<Unit> onFailurLoadTalker = new();

        public IObservable<Unit> OnLoadedWindow => onLoadedWindow;
        public IObservable<Unit> OnFailureLoadTalker => onFailurLoadTalker;
        public ReactivePropertySlim<string> StatusText { get; } = new( "CeVIO AI に接続しています...");
        public ReactivePropertySlim<bool> StartButtonIsEnabled { get; } = new(false);
        public ReactivePropertySlim<Brush> StartButtonForeground { get; } = new(Brushes.Black);
        public ReactivePropertySlim<Brush> StartButtonBackground { get; } = new(Brushes.Gray);

        public void CompleteLoadedWindow()
        {
            onLoadedWindow.OnNext(Unit.Default);
        }

        public void CompleteLoadedTalker()
        {
            StatusText.Value = "CeVIO AI との接続が完了しました！";
            StartButtonIsEnabled.Value = true;
            StartButtonForeground.Value = Brushes.White;
            StartButtonBackground.Value = AccentColors.ImmersiveSystemAccentBrush;
        }

        public void FailureLoadTalker()
        {
            onFailurLoadTalker.OnNext(Unit.Default);
        }
    }
}
