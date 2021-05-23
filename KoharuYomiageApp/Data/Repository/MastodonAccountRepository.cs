﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository.DataObjects;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.Account.Mastodon;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class MastodonAccountRepository : IMastodonAccountRepository
    {
        readonly IMastodonAccountStorage _storage;

        public MastodonAccountRepository(IMastodonAccountStorage storage)
        {
            _storage = storage;
        }

        public async Task<MastodonAccount?> FindMastodonAccount(AccountIdentifier identifier)
        {
            var data = await _storage.FindMastodonAccountData(identifier.Value);
            if (data is not null)
            {
                return new MastodonAccount(new Username(data.Username), new Instance(data.Instance),
                    new MastodonAccessToken(data.AccessToken), new MastodonAccountIconUrl(data.IconUrl));
            }

            return null;
        }

        public MastodonAccount CreateMastodonAccount(Username username, Instance instance,
            MastodonAccessToken accessToken,
            MastodonAccountIconUrl iconUrl)
        {
            return new(username, instance, accessToken, iconUrl);
        }

        public async Task SaveMastodonAccount(MastodonAccount accountData)
        {
            await _storage.SaveMastodonAccountData(new MastodonAccountData(accountData.Username.Value,
                accountData.Instance.Value, accountData.AccessToken.Token, accountData.IconUrl.IconUrl));
        }

        public async Task<IEnumerable<MastodonAccount>> GetAllMastodonAccounts()
        {
            var data = await _storage.GetMastodonAccountData();
            return data.Select(d => new MastodonAccount(new Username(d.Username), new Instance(d.Instance),
                new MastodonAccessToken(d.AccessToken), new MastodonAccountIconUrl(d.IconUrl)));
        }
    }
}