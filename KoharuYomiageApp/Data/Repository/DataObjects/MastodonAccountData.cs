using System;

namespace KoharuYomiageApp.Data.Repository.DataObjects
{
    public record MastodonAccountData(string Username, string Instance, string AccessToken, Uri IconUrl,
        bool IsReadingPostsFromThisAccount);
}
