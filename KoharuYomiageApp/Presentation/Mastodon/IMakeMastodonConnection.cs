using System;

namespace KoharuYomiageApp.Presentation.Mastodon
{
    public interface IMakeMastodonConnection
    {
        IDisposable MakeConnection(string username, string instance, string accessToken);
    }
}
