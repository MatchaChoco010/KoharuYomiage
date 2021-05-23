using System;
using System.Collections.Generic;
using KoharuYomiageApp.Domain.Account;

namespace KoharuYomiageApp.Domain.Connection
{
    public class ConnectionManager : IDisposable
    {
        readonly Dictionary<AccountIdentifier, IDisposable> _connections = new();

        public void Dispose()
        {
            foreach (var item in _connections)
            {
                item.Value.Dispose();
            }
        }

        public void AddConnection(AccountIdentifier accountId, IDisposable connection)
        {
            _connections.Add(accountId, connection);
        }

        public void StopConnection(AccountIdentifier accountId)
        {
            _connections[accountId].Dispose();
            _connections.Remove(accountId);
        }
    }
}
