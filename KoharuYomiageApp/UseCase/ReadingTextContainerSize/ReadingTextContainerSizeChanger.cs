using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.ReadingTextContainerSize
{
    public class ReadingTextContainerSizeChanger : IChangeReadingTextContainerSize
    {
        readonly IReadingTextContainerRepository _readingTextContainerRepository;

        public ReadingTextContainerSizeChanger(IReadingTextContainerRepository readingTextContainerRepository)
        {
            _readingTextContainerRepository = readingTextContainerRepository;
        }

        public async Task ChangeContainerSize(int size, CancellationToken cancellationToken)
        {
            var containerSize = _readingTextContainerRepository.CreateContainerSize(size);
            await _readingTextContainerRepository.SaveContainerSize(containerSize, cancellationToken);
        }
    }
}
