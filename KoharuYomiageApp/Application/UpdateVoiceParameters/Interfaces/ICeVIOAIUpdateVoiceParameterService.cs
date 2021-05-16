using KoharuYomiageApp.Application.UpdateVoiceParameters.Interfaces.DataObjects;

namespace KoharuYomiageApp.Application.UpdateVoiceParameters.Interfaces
{
    public interface ICeVIOAIUpdateVoiceParameterService
    {
        void Update(VoiceParameterOutputData data);
    }
}
