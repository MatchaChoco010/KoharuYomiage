using System;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public record MastodonAccountData(string Username, string Instance, string AccessToken, Uri IconUrl);
}
