using System;
using KoharuYomiageApp.Domain.Account;

namespace KoharuYomiageApp.Domain.Connection
{
    public class Connection : IDisposable
    {
        readonly IDisposable _connection;

        public AccountIdentifier AccountIdentifier { get; }

        public Connection(AccountIdentifier accountId, IDisposable connection)
        {
            AccountIdentifier = accountId;
            _connection = connection;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
