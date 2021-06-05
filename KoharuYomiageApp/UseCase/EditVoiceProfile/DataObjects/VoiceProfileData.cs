namespace KoharuYomiageApp.UseCase.EditVoiceProfile.DataObjects
{
    public record VoiceProfileData(double Volume, double Speed, double Tone, double Alpha, double ToneScale,
        double ComponentNormal, double ComponentHappy, double ComponentAnger, double ComponentSorrow,
        double ComponentCalmness);
}
