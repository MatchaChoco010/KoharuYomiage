using KoharuYomiageApp.Entities.ReadingText;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public class ReadingTextContainerRepository
    {
        readonly ReadingTextContainer _container = new();

        public ReadingTextContainer GetContainer()
        {
            return _container;
        }
    }
}
