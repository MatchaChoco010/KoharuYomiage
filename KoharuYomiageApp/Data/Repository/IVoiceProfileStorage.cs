using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository.DataObjects;

namespace KoharuYomiageApp.Data.Repository
{
    public interface IVoiceProfileStorage
    {
        Task<VoiceProfileData?> FindVoiceProfile(string accountIdentifier, string type,
            CancellationToken cancellationToken);

        Task<IEnumerable<VoiceProfileData>> GetVoiceProfiles(string accountIdentifier,
            CancellationToken cancellationToken);

        Task SaveVoiceProfile(VoiceProfileData data, CancellationToken cancellationToken);
    }
}
