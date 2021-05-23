using System;
using KoharuYomiageApp.UseCase.WindowLoaded.DataObjects;

namespace KoharuYomiageApp.UseCase.WindowLoaded
{
    public interface IMakeMastodonConnection
    {
        IDisposable MakeConnection(AddReaderInfo account);
    }
}
