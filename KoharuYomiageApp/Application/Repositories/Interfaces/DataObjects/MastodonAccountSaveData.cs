using System;

namespace KoharuYomiageApp.Application.Repositories.Interfaces.DataObjects
{
    public record MastodonAccountSaveData(string Username, string Instance, string AccessToken, Uri IconUrl);
}
