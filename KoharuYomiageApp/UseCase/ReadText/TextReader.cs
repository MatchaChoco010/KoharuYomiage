using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.ReadText
{
    public class TextReader : IStartReading
    {
        readonly IChangeImage _changeImage;
        readonly IReadingTextContainerRepository _containerRepository;
        readonly ISpeakText _speakText;

        public TextReader(IReadingTextContainerRepository containerRepository, ISpeakText speakText,
            IChangeImage changeImage)
        {
            _containerRepository = containerRepository;
            _speakText = speakText;
            _changeImage = changeImage;
        }

        public async Task StartReading(CancellationToken cancellationToken)
        {
            var container = _containerRepository.GetContainer();

            while (!cancellationToken.IsCancellationRequested)
            {
                var item = await container.PeekAsync(cancellationToken);

                try
                {
                    _changeImage.OpenMouth();
                    var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                    await Task.WhenAny(_speakText.SpeakText(item.Text, cts.Token), container.Overflow(cts.Token));
                    cts.Cancel(true);
                    cts.Dispose();
                }
                finally
                {
                    _changeImage.CloseMouth();
                    container.RemoveFirstItem();
                }
            }
        }
    }
}
