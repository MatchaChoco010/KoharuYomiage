using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public class GlobalVolumeGateway : IGlobalVolumeGateway
    {
        readonly IGlobalVolumeStorage _storage;

        public GlobalVolumeGateway(IGlobalVolumeStorage storage)
        {
            _storage = storage;
        }

        public Task<double?> FindGlobalVolume()
        {
            return _storage.FindGlobalVolume();
        }

        public Task SaveGlobalVolume(double volume)
        {
            return _storage.SaveGlobalVolume(volume);
        }
    }
}
