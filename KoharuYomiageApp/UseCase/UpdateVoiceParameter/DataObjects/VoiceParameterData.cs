namespace KoharuYomiageApp.UseCase.UpdateVoiceParameter.DataObjects
{
    public record VoiceParameterData(uint Volume, uint Speed, uint Tone, uint Alpha, uint ToneScale,
        uint ComponentNormal, uint ComponentHappy, uint ComponentAnger, uint ComponentSorrow, uint ComponentCalmness);
}
