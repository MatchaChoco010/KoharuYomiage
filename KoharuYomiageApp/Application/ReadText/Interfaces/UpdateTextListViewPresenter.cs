using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.ReadText.UseCases;

namespace KoharuYomiageApp.Application.ReadText.Interfaces
{
    public class UpdateTextListViewPresenter : IUpdateTextListView
    {
        readonly Subject<(Guid, string)> _onAddItem = new();
        readonly Subject<(Guid, string)> _onDeleteItem = new();

        List<(Guid, string)> _prevList = new();

        public IObservable<(Guid, string)> OnDeleteItem => _onDeleteItem;
        public IObservable<(Guid, string)> OnAddItem => _onAddItem;

        public void Update(IEnumerable<(Guid, string)> list)
        {
            var itemList = list.ToList();

            var deleteList = _prevList.Where(item => !itemList.Contains(item));
            foreach (var item in deleteList)
            {
                _onDeleteItem.OnNext(item);
            }

            var addList = itemList.Where(item => !_prevList.Contains(item));
            foreach (var item in addList)
            {
                _onAddItem.OnNext(item);
            }

            _prevList = itemList;
        }
    }
}
