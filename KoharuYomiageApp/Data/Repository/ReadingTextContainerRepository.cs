using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.ReadingText;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class ReadingTextContainerRepository : IDisposable, IReadingTextContainerRepository
    {
        readonly ReadingTextContainer _container = new();
        readonly IReadingTextContainerStorage _storage;

        public ReadingTextContainerRepository(IReadingTextContainerStorage storage)
        {
            _storage = storage;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public ReadingTextContainer GetContainer()
        {
            return _container;
        }

        public ReadingTextContainerSize CreateContainerSize(int size)
        {
            return new(size);
        }

        public async Task<ReadingTextContainerSize> GetContainerSize(CancellationToken cancellationToken)
        {
            var data = await _storage.FindReadingTextContainerSize(cancellationToken);
            return data is { } size ? new ReadingTextContainerSize(size) : new ReadingTextContainerSize(3);
        }

        public async Task SaveContainerSize(ReadingTextContainerSize size, CancellationToken cancellationToken)
        {
            await _storage.SaveReadingTextContainerSize(size.Value, cancellationToken);
        }
    }
}
