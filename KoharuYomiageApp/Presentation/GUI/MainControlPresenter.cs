using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.Text.RegularExpressions;
using KoharuYomiageApp.UseCase.ReadText;
using KoharuYomiageApp.UseCase.UpdateTextList;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MainControlPresenter : IChangeImage, IUpdateTextListView
    {
        readonly Subject<Unit> _onCloseMouth = new();
        readonly Subject<Unit> _onOpenMouth = new();
        readonly BehaviorSubject<List<(Guid, string)>> _textListSubject = new(new List<(Guid, string)>());

        public IObservable<Unit> OnOpenMouth => _onOpenMouth;
        public IObservable<Unit> OnCloseMouth => _onCloseMouth;
        public IObservable<List<(Guid, string)>> TextListAsObservable => _textListSubject;

        public void OpenMouth()
        {
            _onOpenMouth.OnNext(Unit.Default);
        }

        public void CloseMouth()
        {
            _onCloseMouth.OnNext(Unit.Default);
        }

        public void UpdateTextList(IEnumerable<(Guid, string)> list)
        {
            _textListSubject.OnNext(list.Select(t =>
            {
                var (id, text) = t;
                text = Regex.Replace(text, "<br[^>]*?>", "\n");
                text = Regex.Replace(text, "</p>", "</p>\n\n");
                text = Regex.Replace(text, "<[^>]*?>", "");
                return (id, text);
            }).ToList());
        }
    }
}
