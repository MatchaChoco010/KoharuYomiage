using System.Threading.Tasks;

namespace KoharuYomiageApp.Data.Repository
{
    public interface IGlobalVolumeStorage
    {
        Task<double?> FindGlobalVolume();
        Task SaveGlobalVolume(double volume);
    }
}
