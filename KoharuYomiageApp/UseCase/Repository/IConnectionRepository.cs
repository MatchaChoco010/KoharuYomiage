using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Connection;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IConnectionRepository
    {
        void AddConnection(Connection connection);
        void StopConnection(AccountIdentifier accountId);
    }
}
