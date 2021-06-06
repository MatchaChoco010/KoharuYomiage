using System;

namespace KoharuYomiageApp.Data.Repository.DataObjects
{
    public record MastodonAccountData(string Username, string Instance, string DisplayName, string AccessToken,
        Uri IconUrl, bool IsReadingPostsFromThisAccount);
}
