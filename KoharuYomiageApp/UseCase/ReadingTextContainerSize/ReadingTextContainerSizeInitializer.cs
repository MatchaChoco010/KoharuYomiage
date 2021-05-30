using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.ReadingTextContainerSize
{
    public class ReadingTextContainerSizeInitializer : IInitializeReadingTextContainerSize
    {
        readonly IReadingTextContainerRepository _readingTextContainerRepository;

        public ReadingTextContainerSizeInitializer(IReadingTextContainerRepository readingTextContainerRepository)
        {
            _readingTextContainerRepository = readingTextContainerRepository;
        }

        public async Task InitializeReadingTextContainerSize(CancellationToken cancellationToken)
        {
            var size = await _readingTextContainerRepository.GetContainerSize(cancellationToken);
            var container = _readingTextContainerRepository.GetContainer();
            container.MaxCount = size;
        }
    }
}
