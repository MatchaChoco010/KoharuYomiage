using KoharuYomiageApp.Domain.ReadingText;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class ReadingTextContainerRepository : IReadingTextContainerRepository
    {
        readonly ReadingTextContainer _container = new();

        public ReadingTextContainer GetContainer()
        {
            return _container;
        }
    }
}
