using KoharuYomiageApp.Domain.ReadingText;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IReadingTextContainerRepository
    {
        ReadingTextContainer GetContainer();
    }
}
