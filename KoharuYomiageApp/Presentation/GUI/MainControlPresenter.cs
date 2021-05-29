using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Subjects;
using System.Text.RegularExpressions;
using KoharuYomiageApp.UseCase.ReadText;
using KoharuYomiageApp.UseCase.UpdateTextList;
using KoharuYomiageApp.UseCase.UpdateVoiceParameter;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MainControlPresenter : IInitializeGlobalVolumeView, IChangeImage, IUpdateTextListView
    {
        readonly Subject<(Guid, string)> _onAddTextListItem = new();
        readonly Subject<Unit> _onCloseMouth = new();
        readonly Subject<(Guid, string)> _onDeleteTextListItem = new();
        readonly BehaviorSubject<double> _onInitializeGlobalVolumeView = new(0);
        readonly Subject<Unit> _onOpenMouth = new();

        List<(Guid, string)> _prevTextList = new();

        public IObservable<double> OnInitializeGlobalVolumeView => _onInitializeGlobalVolumeView;
        public IObservable<Unit> OnOpenMouth => _onOpenMouth;
        public IObservable<Unit> OnCloseMouth => _onCloseMouth;
        public IObservable<(Guid, string)> OnDeleteTextListItem => _onDeleteTextListItem;
        public IObservable<(Guid, string)> OnAddTextListItem => _onAddTextListItem;

        public void OpenMouth()
        {
            _onOpenMouth.OnNext(Unit.Default);
        }

        public void CloseMouth()
        {
            _onCloseMouth.OnNext(Unit.Default);
        }

        public void Initialize(double volume)
        {
            _onInitializeGlobalVolumeView.OnNext(volume);
            _onInitializeGlobalVolumeView.OnCompleted();
        }

        public void UpdateTextList(IEnumerable<(Guid, string)> list)
        {
            var itemList = list.ToList();

            var deleteList = _prevTextList.Where(item => !itemList.Contains(item));
            foreach (var (id, text) in deleteList)
            {
                string filteredText = text;
                filteredText = Regex.Replace(filteredText, "<br[^>]*?>", "\n");
                filteredText = Regex.Replace(filteredText, "</p>", "</p>\n\n");
                filteredText = Regex.Replace(filteredText, "<[^>]*?>", "");
                _onDeleteTextListItem.OnNext((id, filteredText));
            }

            var addList = itemList.Where(item => !_prevTextList.Contains(item));
            foreach (var (id, text) in addList)
            {
                string filteredText = text;
                filteredText = Regex.Replace(filteredText, "<br[^>]*?>", "\n");
                filteredText = Regex.Replace(filteredText, "</p>", "</p>\n\n");
                filteredText = Regex.Replace(filteredText, "<[^>]*?>", "");
                _onAddTextListItem.OnNext((id, filteredText));
            }

            _prevTextList = itemList;
        }
    }
}
