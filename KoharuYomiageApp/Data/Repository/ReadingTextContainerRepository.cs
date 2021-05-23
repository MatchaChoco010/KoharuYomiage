using System;
using KoharuYomiageApp.Domain.ReadingText;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class ReadingTextContainerRepository : IDisposable, IReadingTextContainerRepository
    {
        readonly ReadingTextContainer _container = new();

        public void Dispose()
        {
            _container.Dispose();
        }

        public ReadingTextContainer GetContainer()
        {
            return _container;
        }
    }
}
