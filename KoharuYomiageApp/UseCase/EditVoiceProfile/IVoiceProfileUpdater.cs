using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.EditVoiceProfile.DataObjects;

namespace KoharuYomiageApp.UseCase.EditVoiceProfile
{
    public interface IVoiceProfileUpdater
    {
        Task SetVoiceProfile(string username, string instance, VoiceProfileType type, VoiceProfileData data, CancellationToken cancellationToken);
        Task<VoiceProfileData> GetVoiceProfile(string username, string instance, VoiceProfileType type, CancellationToken cancellationToken);
        Task PlaySampleVoice(string username, string instance, VoiceProfileType type, string sampleText, CancellationToken cancellationToken);
    }
}
