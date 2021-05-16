using KoharuYomiageApp.Application.UpdateVoiceParameters.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.UpdateVoiceParameters.UseCases
{
    public interface IUpdateVoiceParameter
    {
        void Update(VoiceParameterData data);
    }
}
