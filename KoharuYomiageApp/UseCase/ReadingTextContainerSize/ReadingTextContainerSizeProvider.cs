using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.ReadingTextContainerSize
{
    public class ReadingTextContainerSizeProvider : IGetReadingTextContainerSize
    {
        readonly IReadingTextContainerRepository _readingTextContainerRepository;

        public ReadingTextContainerSizeProvider(IReadingTextContainerRepository readingTextContainerRepository)
        {
            _readingTextContainerRepository = readingTextContainerRepository;
        }

        public async Task<int> GetReadingTextContainerSize(CancellationToken cancellationToken)
        {
            var size = await _readingTextContainerRepository.GetContainerSize(cancellationToken);
            return size.Value;
        }
    }
}
