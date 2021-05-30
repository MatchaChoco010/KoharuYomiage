using System;

namespace KoharuYomiageApp.UseCase.Utils
{
    public interface IMakeMastodonConnection
    {
        IDisposable MakeConnection(string username, string instance, string accessToken);
    }
}
