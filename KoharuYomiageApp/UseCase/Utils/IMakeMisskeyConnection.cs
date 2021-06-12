using System;

namespace KoharuYomiageApp.UseCase.Utils
{
    public interface IMakeMisskeyConnection
    {
        IDisposable MakeConnection(string username, string instance, string accessToken);
    }
}
