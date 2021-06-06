namespace KoharuYomiageApp.UseCase.UpdateVoiceParameter.DataObjects
{
    public record VoiceParameterData(float GlobalVolume, uint Volume, uint Speed, uint Tone, uint Alpha, uint ToneScale,
        uint ComponentNormal, uint ComponentHappy, uint ComponentAnger, uint ComponentSorrow, uint ComponentCalmness);
}
