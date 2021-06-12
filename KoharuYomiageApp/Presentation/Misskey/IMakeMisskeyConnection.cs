using System;

namespace KoharuYomiageApp.Presentation.Misskey
{
    public interface IMakeMisskeyConnection
    {
        IDisposable MakeConnection(string username, string instance, string accessToken);
    }
}
