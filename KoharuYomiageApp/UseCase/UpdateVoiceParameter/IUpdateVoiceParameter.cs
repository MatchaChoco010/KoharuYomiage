using KoharuYomiageApp.UseCase.UpdateVoiceParameter.DataObjects;

namespace KoharuYomiageApp.UseCase.UpdateVoiceParameter
{
    public interface IUpdateVoiceParameter
    {
        void Update(VoiceParameterData data);
    }
}
