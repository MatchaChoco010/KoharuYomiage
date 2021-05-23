using KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonAccount
{
    public interface IShowAuthUrl
    {
        void ShowAuthUrl(AuthorizationUrl authorizationUrl);
    }
}
