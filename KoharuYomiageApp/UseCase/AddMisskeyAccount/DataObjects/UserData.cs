using System;

namespace KoharuYomiageApp.UseCase.AddMisskeyAccount.DataObjects
{
    public record UserData(string Username, string DisplayName, Uri IconUrl);
}
