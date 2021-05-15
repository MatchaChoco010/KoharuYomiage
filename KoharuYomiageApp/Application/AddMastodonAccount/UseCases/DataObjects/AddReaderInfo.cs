namespace KoharuYomiageApp.Application.AddMastodonAccount.UseCases.DataObjects
{
    public record AddReaderInfo(string AccountIdentifier, string Username, string Instance, string AccessToken);
}
