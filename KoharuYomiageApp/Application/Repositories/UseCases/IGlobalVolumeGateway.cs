using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public interface IGlobalVolumeGateway
    {
        Task<double?> FindGlobalVolume();
        Task SaveGlobalVolume(double volume);
    }
}
