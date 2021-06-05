using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.VoiceParameters;
using KoharuYomiageApp.UseCase.EditVoiceProfile.DataObjects;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.EditVoiceProfile
{
    public class VoiceProfileUpdater : IVoiceProfileUpdater
    {
        readonly IVoiceProfileRepository _repository;

        public VoiceProfileUpdater(IVoiceProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task SetVoiceProfile(string username, string instance, VoiceProfileType type, VoiceProfileData data, CancellationToken cancellationToken)
        {
            var id = new AccountIdentifier(new Username(username), new Instance(instance));
            var profile = type switch
            {
                VoiceProfileType.MastodonStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonSensitiveStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonBoostedStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonBoostedStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonBoostedSensitiveStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile>(id, cancellationToken),
                _ => throw new InvalidProgramException(),
            };

            profile.Update(data.Volume, data.Speed, data.Tone, data.Alpha, data.ToneScale, data.ComponentNormal,
                data.ComponentHappy, data.ComponentAnger, data.ComponentSorrow, data.ComponentCalmness);

            System.Diagnostics.Debug.WriteLine(profile);

            await _repository.SaveVoiceProfile(profile, cancellationToken);
        }

        public async Task<VoiceProfileData> GetVoiceProfile(string username, string instance, VoiceProfileType type, CancellationToken cancellationToken)
        {
            var id = new AccountIdentifier(new Username(username), new Instance(instance));
            var profile = type switch
            {
                VoiceProfileType.MastodonStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonSensitiveStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonSensitiveStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonBoostedStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonBoostedStatusVoiceProfile>(id, cancellationToken),
                VoiceProfileType.MastodonBoostedSensitiveStatus => await _repository
                    .GetVoiceProfile<VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile>(id, cancellationToken),
                _ => throw new InvalidProgramException(),
            };

            return new VoiceProfileData(profile.Volume, profile.Speed, profile.Tone, profile.Alpha, profile.ToneScale,
                profile.ComponentNormal, profile.ComponentHappy, profile.ComponentAnger, profile.ComponentSorrow,
                profile.ComponentCalmness);
        }
    }
}
