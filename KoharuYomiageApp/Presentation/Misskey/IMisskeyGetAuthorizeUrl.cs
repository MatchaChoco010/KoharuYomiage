using System;
using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Presentation.Misskey
{
    public interface IMisskeyGetAuthorizeUrl
    {
        Task<(string, Uri)> GetAuthorizeUri(string hostName, CancellationToken cancellationToken = new());
    }
}
