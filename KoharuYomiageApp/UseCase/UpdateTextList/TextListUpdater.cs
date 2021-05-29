using System;
using System.Linq;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.UpdateTextList
{
    public class TextListUpdater : IStartUpdatingTextList
    {
        readonly IReadingTextContainerRepository _containerRepository;
        readonly IUpdateTextListView _updateTextListView;

        public TextListUpdater(IReadingTextContainerRepository containerRepository,
            IUpdateTextListView updateTextListView)
        {
            _containerRepository = containerRepository;
            _updateTextListView = updateTextListView;
        }

        public IDisposable StartUpdatingTextList()
        {
            var container = _containerRepository.GetContainer();

            return container.OnItemsChange.Subscribe(items =>
                _updateTextListView.UpdateTextList(items.Select(i => (i.Id, i.Text))));
        }
    }
}
