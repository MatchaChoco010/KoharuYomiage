using System;
using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.AddMisskeyAccount
{
    public interface IGetAuthorizeUrl
    {
        Task<(string, Uri)> GetAuthorizeUri(string hostName, string secret, CancellationToken cancellationToken);
    }
}
