namespace KoharuYomiageApp.Presentation.CeVIOAI.DataObjects
{
    public record VoiceParameterOutputData(float GlobalVolume, uint Volume, uint Speed, uint Tone, uint Alpha,
        uint ToneScale, uint ComponentNormal, uint ComponentHappy, uint ComponentAnger, uint ComponentSorrow,
        uint ComponentCalmness);
}
