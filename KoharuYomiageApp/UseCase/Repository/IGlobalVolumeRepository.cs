using System.Threading.Tasks;
using KoharuYomiageApp.Domain.VoiceParameters;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IGlobalVolumeRepository
    {
        Task<GlobalVolume> GetGlobalVolume();
        Task SaveGlobalVolume(GlobalVolume volume);
    }
}
