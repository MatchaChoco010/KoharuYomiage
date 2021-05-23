namespace KoharuYomiageApp.UseCase.AddMastodonAccount.DataObjects
{
    public record AuthorizationInfo(string Instance, string ClientId, string ClientSecret, string AuthorizationCode);
}
