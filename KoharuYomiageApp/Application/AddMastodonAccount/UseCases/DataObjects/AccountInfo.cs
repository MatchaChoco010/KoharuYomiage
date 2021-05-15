using System;

namespace KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects
{
    public record AccountInfo(string Username, Uri IconUrl);
}
