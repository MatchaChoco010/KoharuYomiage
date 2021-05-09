using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.LoadTalker.UseCases;

namespace KoharuYomiageApp.Application.LoadTalker.Interfaces
{
    public class LoadTalkerPresenter : ILoadTalkerOutputBoundary
    {
        readonly Subject<Unit> onLoadedTalker = new();
        readonly Subject<Unit> onLoadedWindow = new();

        public IObservable<Unit> OnLoadedWindow => onLoadedWindow;
        public IObservable<Unit> OnLoadedTalker => onLoadedTalker;

        public void CompleteLoadedWindow()
        {
            onLoadedWindow.OnNext(Unit.Default);
        }

        public void CompleteLoadedTalker()
        {
            onLoadedTalker.OnNext(Unit.Default);
        }
    }
}
