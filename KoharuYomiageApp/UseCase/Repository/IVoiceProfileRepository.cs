using System.Collections.Generic;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.VoiceParameters;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IVoiceProfileRepository
    {
        Task<VoiceProfile> GetVoiceProfile<T>(AccountIdentifier accountIdentifier)
            where T : VoiceProfile;

        Task<IEnumerable<VoiceProfile>> GetVoiceProfiles(AccountIdentifier accountIdentifier);
        Task SaveVoiceProfile(VoiceProfile profile);
    }
}
