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
        readonly Subject<(Guid, string)> _onAddTextListItem = new();
        readonly Subject<Unit> _onCloseMouth = new();
        readonly Subject<(Guid, string)> _onDeleteTextListItem = new();
        readonly Subject<Unit> _onOpenMouth = new();

        List<(Guid, string)> _currentTextList = new();

        public IObservable<Unit> OnOpenMouth => _onOpenMouth;
        public IObservable<Unit> OnCloseMouth => _onCloseMouth;
        public IObservable<(Guid, string)> OnDeleteTextListItem => _onDeleteTextListItem;
        public IObservable<(Guid, string)> OnAddTextListItem => _onAddTextListItem;
        public IEnumerable<(Guid, string)> CurrentTextList => _currentTextList;

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
            var newItemList = list.ToList();

            var deleteList = _currentTextList.Where(item => !newItemList.Contains(item));
            foreach (var (id, text) in deleteList)
            {
                string filteredText = text;
                filteredText = Regex.Replace(filteredText, "<br[^>]*?>", "\n");
                filteredText = Regex.Replace(filteredText, "</p>", "</p>\n\n");
                filteredText = Regex.Replace(filteredText, "<[^>]*?>", "");
                _onDeleteTextListItem.OnNext((id, filteredText));
            }

            var addList = newItemList.Where(item => !_currentTextList.Contains(item));
            foreach (var (id, text) in addList)
            {
                string filteredText = text;
                filteredText = Regex.Replace(filteredText, "<br[^>]*?>", "\n");
                filteredText = Regex.Replace(filteredText, "</p>", "</p>\n\n");
                filteredText = Regex.Replace(filteredText, "<[^>]*?>", "");
                _onAddTextListItem.OnNext((id, filteredText));
            }

            _currentTextList = newItemList;
        }
    }
}
