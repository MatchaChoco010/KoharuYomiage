using System;
using System.Reactive;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.ReadText.UseCases;

namespace KoharuYomiageApp.Application.ReadText.Interfaces
{
    public class ChangeImagePresenter : IChangeImage
    {
        readonly Subject<Unit> _onCloseMouth = new();
        readonly Subject<Unit> _onOpenMouth = new();

        public IObservable<Unit> OnOpenMouth => _onOpenMouth;
        public IObservable<Unit> OnCloseMouth => _onCloseMouth;

        public void OpenMouth()
        {
            _onOpenMouth.OnNext(Unit.Default);
        }

        public void CloseMouth()
        {
            _onCloseMouth.OnNext(Unit.Default);
        }
    }
}
