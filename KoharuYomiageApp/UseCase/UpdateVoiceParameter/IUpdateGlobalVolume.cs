using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.UpdateVoiceParameter
{
    public interface IUpdateGlobalVolume
    {
        Task Update(double volume);
    }
}
