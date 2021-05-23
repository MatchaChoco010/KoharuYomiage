using System.Collections.Generic;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository.DataObjects;

namespace KoharuYomiageApp.Data.Repository
{
    public interface IVoiceProfileStorage
    {
        Task<VoiceProfileData?> FindVoiceProfile(string accountIdentifier, string type);
        Task<IEnumerable<VoiceProfileData>> GetVoiceProfiles(string accountIdentifier);
        Task SaveVoiceProfile(VoiceProfileData data);
    }
}
