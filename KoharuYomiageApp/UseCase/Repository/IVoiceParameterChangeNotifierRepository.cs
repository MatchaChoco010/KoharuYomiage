using System.Threading.Tasks;
using KoharuYomiageApp.Domain.VoiceParameters;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IVoiceParameterChangeNotifierRepository
    {
        ValueTask<VoiceParameterChangeNotifier> GetInstance();
    }
}
