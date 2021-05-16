using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public interface IGlobalVolumeStorage
    {
        Task<double?> FindGlobalVolume();
        Task SaveGlobalVolume(double volume);
    }
}
