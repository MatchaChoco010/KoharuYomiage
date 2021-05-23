using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository.DataObjects;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.VoiceParameters;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class VoiceProfileRepository : IVoiceProfileRepository
    {
        readonly IVoiceProfileStorage _storage;

        public VoiceProfileRepository(IVoiceProfileStorage storage)
        {
            _storage = storage;
        }

        public async Task<VoiceProfile> GetVoiceProfile<T>(AccountIdentifier accountIdentifier)
            where T : VoiceProfile
        {
            var type = typeof(T) switch
            {
                var c when c == typeof(VoiceProfile.MastodonStatusVoiceProfile) => "MastodonStatus",
                var c when c == typeof(VoiceProfile.MastodonSensitiveStatusVoiceProfile) => "MastodonSensitiveStatus",
                var c when c == typeof(VoiceProfile.MastodonBoostedStatusVoiceProfile) => "MastodonBoostedStatus",
                var c when c == typeof(VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile) =>
                    "MastodonBoostedSensitiveStatus",
                _ => throw new ArgumentException()
            };

            VoiceProfile profile = type switch
            {
                "MastodonStatus" => new VoiceProfile.MastodonStatusVoiceProfile(accountIdentifier),
                "MastodonSensitiveStatus" => new VoiceProfile.MastodonSensitiveStatusVoiceProfile(accountIdentifier),
                "MastodonBoostedStatus" => new VoiceProfile.MastodonBoostedStatusVoiceProfile(accountIdentifier),
                "MastodonBoostedSensitiveStatus" => new VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile(
                    accountIdentifier),
                _ => throw new ArgumentException()
            };

            var data = await _storage.FindVoiceProfile(accountIdentifier.Value, type);
            if (data is null)
            {
                return profile;
            }

            profile.Update(data.Volume, data.Speed, data.Tone, data.Alpha, data.ToneScale, data.ComponentNormal,
                data.ComponentHappy, data.ComponentAnger, data.ComponentSorrow, data.ComponentCalmness);
            return profile;
        }

        public async Task<IEnumerable<VoiceProfile>> GetVoiceProfiles(AccountIdentifier accountIdentifier)
        {
            var data = await _storage.GetVoiceProfiles(accountIdentifier.Value);
            return data.Select(d =>
            {
                VoiceProfile profile = d.Type switch
                {
                    "MastodonStatus" => new VoiceProfile.MastodonStatusVoiceProfile(accountIdentifier),
                    "MastodonSensitiveStatus" =>
                        new VoiceProfile.MastodonSensitiveStatusVoiceProfile(accountIdentifier),
                    "MastodonBoostedStatus" => new VoiceProfile.MastodonBoostedStatusVoiceProfile(accountIdentifier),
                    "MastodonBoostedSensitiveStatus" => new VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile(
                        accountIdentifier),
                    _ => throw new AggregateException()
                };
                profile.Update(d.Volume, d.Speed, d.Tone, d.Alpha, d.ToneScale, d.ComponentNormal, d.ComponentHappy,
                    d.ComponentAnger, d.ComponentSorrow, d.ComponentCalmness);
                return profile;
            });
        }

        public async Task SaveVoiceProfile(VoiceProfile profile)
        {
            var type = profile switch
            {
                VoiceProfile.MastodonStatusVoiceProfile => "MastodonStatus",
                VoiceProfile.MastodonSensitiveStatusVoiceProfile => "MastodonSensitiveStatus",
                VoiceProfile.MastodonBoostedStatusVoiceProfile => "MastodonBoostedStatus",
                VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile => "MastodonBoostedSensitiveStatus",
                _ => throw new ArgumentException()
            };
            var data = new VoiceProfileData(profile.AccountIdentifier.Value, type, profile.Volume, profile.Speed,
                profile.Tone, profile.Alpha, profile.ToneScale, profile.ComponentNormal, profile.ComponentHappy,
                profile.ComponentAnger, profile.ComponentSorrow, profile.ComponentCalmness);
            await _storage.SaveVoiceProfile(data);
        }
    }
}
