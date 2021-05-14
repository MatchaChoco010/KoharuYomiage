using KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonAccount.UseCases
{
    public interface IShowAuthUrl
    {
        void ShowAuthUrl(AuthUrl authUrl);
    }
}
