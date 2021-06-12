using System;

namespace KoharuYomiageApp.UseCase.AddMisskeyAccount
{
    public interface IShowAuthUrl
    {
        void ShowAuthUrl(Uri authorizationUrl);
    }
}
