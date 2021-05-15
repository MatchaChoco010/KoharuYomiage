namespace KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects
{
    public record AuthorizationInfo(string Instance, string ClientId, string ClientSecret, string AuthorizationCode);
}
