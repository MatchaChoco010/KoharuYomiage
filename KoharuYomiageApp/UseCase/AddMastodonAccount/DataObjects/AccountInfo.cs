using System;

namespace KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects
{
    public record AccountInfo(string Username, string DisplayName, Uri IconUrl);
}
