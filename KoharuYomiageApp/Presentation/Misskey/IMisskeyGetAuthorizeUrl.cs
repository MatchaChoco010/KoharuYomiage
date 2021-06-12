using System;
using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Presentation.Misskey
{
    public interface IMisskeyGetAuthorizeUrl
    {
        Task<(string, Uri)> GetAuthorizeUri(string hostName, string secret,
            CancellationToken cancellationToken = new());
    }
}
