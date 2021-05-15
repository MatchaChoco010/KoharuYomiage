using System;

namespace KoharuYomiageApp.Application.Repositories.UseCases.DataObjects
{
    public record MastodonAccountData(string Username, string Instance, string AccessToken, Uri IconUrl);
}
