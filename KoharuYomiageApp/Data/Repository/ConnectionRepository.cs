using System;
using System.Collections.Generic;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Connection;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class ConnectionRepository : IDisposable, IConnectionRepository
    {
        readonly Dictionary<AccountIdentifier, Connection> _connections = new();

        public void Dispose()
        {
            foreach (var item in _connections)
            {
                item.Value.Dispose();
            }
        }

        public void AddConnection(Connection connection)
        {
            _connections.Add(connection.AccountIdentifier, connection);
        }

        public void StopConnection(AccountIdentifier accountId)
        {
            _connections[accountId].Dispose();
            _connections.Remove(accountId);
        }
    }
}
