using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases;

namespace KoharuYomiageApp.Application.ReadText.UseCases
{
    public class TextReader : IStartReading
    {
        readonly IChangeImage _changeImage;
        readonly ReadingTextContainerRepository _containerRepository;
        readonly ISpeakText _speakText;
        readonly IUpdateTextListView _updateTextListView;

        public TextReader(ReadingTextContainerRepository containerRepository, ISpeakText speakText,
            IUpdateTextListView updateTextListView, IChangeImage changeImage)
        {
            _containerRepository = containerRepository;
            _speakText = speakText;
            _updateTextListView = updateTextListView;
            _changeImage = changeImage;
        }

        public async Task StartReading(CancellationToken cancellationToken)
        {
            var container = _containerRepository.GetContainer();

            var disposable = container.OnItemsChange.Subscribe(items =>
                _updateTextListView.Update(items.Select(i => (i.Id, i.Text))));

            cancellationToken.Register(() => disposable.Dispose());

            while (true)
            {
                var item = await container.GetAsync(cancellationToken);

                _changeImage.OpenMouth();

                var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                await Task.WhenAny(_speakText.SpeakText(item.Text, cts.Token), container.Overflow(cts.Token));
                cts.Cancel();

                _changeImage.CloseMouth();

                container.RemoveFirstItem();
            }
        }
    }
}
