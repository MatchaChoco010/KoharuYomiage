using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.VoiceParameters;

namespace KoharuYomiageApp.UseCase.Repository
{
    public interface IVoiceProfileRepository
    {
        ValueTask<VoiceProfile> GetVoiceProfile<T>(AccountIdentifier accountIdentifier,
            CancellationToken cancellationToken)
            where T : VoiceProfile;

        Task<IEnumerable<VoiceProfile>> GetVoiceProfiles(AccountIdentifier accountIdentifier,
            CancellationToken cancellationToken);

        Task SaveVoiceProfile(VoiceProfile profile, CancellationToken cancellationToken);
    }
}
