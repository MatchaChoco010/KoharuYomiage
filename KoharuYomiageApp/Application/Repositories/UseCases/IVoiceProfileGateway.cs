using System.Collections.Generic;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.Repositories.UseCases
{
    public interface IVoiceProfileGateway
    {
        Task<VoiceProfileData?> FindVoiceProfile(string accountIdentifier, string type);
        Task<IEnumerable<VoiceProfileData>> GetVoiceProfiles(string accountIdentifier);
        Task SaveVoiceProfile(VoiceProfileData data);
    }
}
