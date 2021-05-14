using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Entities;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public class MastodonClientRepository : IMastodonClientRepository
    {
        public MastodonClient CreateMastodonClient(Instance instance, MastodonClientId clientId,
            MastodonClientSecret clientSecret)
        {
            return new(instance, clientId, clientSecret);
        }
    }
}
