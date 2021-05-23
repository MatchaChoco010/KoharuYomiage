using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Data.Repository
{
    public interface IGlobalVolumeStorage
    {
        Task<double?> FindGlobalVolume(CancellationToken cancellationToken);
        Task SaveGlobalVolume(double volume, CancellationToken cancellationToken);
    }
}
