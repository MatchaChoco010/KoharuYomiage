using System;

namespace KoharuYomiageApp.UseCase.GetAllAccounts.DataObjects
{
    public record AccountData(string Id, string Username, string Instance, Uri IconUrl,
        bool IsReadingPostFromThisAccount, string Type);
}
