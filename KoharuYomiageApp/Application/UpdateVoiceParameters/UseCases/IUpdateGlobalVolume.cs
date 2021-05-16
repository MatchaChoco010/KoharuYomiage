using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.UpdateVoiceParameters.UseCases
{
    public interface IUpdateGlobalVolume
    {
        Task Update(double volume);
    }
}
