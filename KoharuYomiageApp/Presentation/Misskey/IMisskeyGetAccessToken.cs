using System;
using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Presentation.Misskey
{
    public interface IMisskeyGetAccessToken
    {
        Task<(string, (string, string, Uri))> GetAccessToken(string instance, string secret, string sessionToken,
            CancellationToken cancellationToken = new());
    }
}
