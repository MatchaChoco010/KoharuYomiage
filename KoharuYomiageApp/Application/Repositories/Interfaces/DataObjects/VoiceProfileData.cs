namespace KoharuYomiageApp.Application.Repositories.Interfaces.DataObjects
{
    public record VoiceProfileSaveData(string AccountIdentifier, string Type, double Volume, double Speed, double Tone,
        double Alpha, double ToneScale, double ComponentNormal, double ComponentHappy, double ComponentAnger,
        double ComponentSorrow, double ComponentCalmness);
}
