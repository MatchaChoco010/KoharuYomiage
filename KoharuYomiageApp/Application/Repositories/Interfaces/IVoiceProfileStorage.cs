using System.Collections.Generic;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.Interfaces.DataObjects;

namespace KoharuYomiageApp.Application.Repositories.Interfaces
{
    public interface IVoiceProfileStorage
    {
        Task<VoiceProfileSaveData?> FindVoiceProfile(string accountIdentifier, string type);
        Task<IEnumerable<VoiceProfileSaveData>> GetVoiceProfiles(string accountIdentifier);
        Task SaveVoiceProfile(VoiceProfileSaveData data);
    }
}
