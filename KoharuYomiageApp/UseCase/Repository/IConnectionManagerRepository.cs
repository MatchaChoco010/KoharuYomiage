using KoharuYomiageApp.Domain.Connection;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IConnectionManagerRepository
    {
        ConnectionManager GetInstance();
    }
}
