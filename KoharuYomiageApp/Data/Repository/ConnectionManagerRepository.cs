using System;
using KoharuYomiageApp.Domain.Connection;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class ConnectionManagerRepository : IDisposable, IConnectionManagerRepository
    {
        readonly ConnectionManager _instance = new();

        public ConnectionManager GetInstance()
        {
            return _instance;
        }

        public void Dispose()
        {
            _instance.Dispose();
        }
    }
}
