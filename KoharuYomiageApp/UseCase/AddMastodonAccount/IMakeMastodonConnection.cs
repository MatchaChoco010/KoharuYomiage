using System;
using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonAccount
{
    public interface IMakeMastodonConnection
    {
        IDisposable MakeConnection(AddReaderInfo addReaderInfo);
    }
}
