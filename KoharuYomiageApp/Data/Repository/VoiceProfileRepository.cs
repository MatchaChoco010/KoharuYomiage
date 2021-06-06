using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Data.Repository.DataObjects;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.VoiceParameters;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.Data.Repository
{
    public class VoiceProfileRepository : IDisposable, IVoiceProfileRepository
    {
        readonly Dictionary<(AccountIdentifier, string), VoiceProfile> _profiles = new();
        readonly SemaphoreSlim _semaphore = new(1, 1);

        readonly IVoiceProfileStorage _storage;

        public VoiceProfileRepository(IVoiceProfileStorage storage)
        {
            _storage = storage;
        }

        public void Dispose()
        {
            _semaphore.Dispose();
            foreach (var item in _profiles)
            {
                item.Value.Dispose();
            }
        }

        public async ValueTask<VoiceProfile> GetVoiceProfile<T>(AccountIdentifier accountIdentifier,
            CancellationToken cancellationToken)
            where T : VoiceProfile
        {
            try
            {
                await _semaphore.WaitAsync(cancellationToken);

                var type = typeof(T) switch
                {
                    var c when c == typeof(VoiceProfile.MastodonStatusVoiceProfile) => "MastodonStatus",
                    var c when c == typeof(VoiceProfile.MastodonSensitiveStatusVoiceProfile) =>
                        "MastodonSensitiveStatus",
                    var c when c == typeof(VoiceProfile.MastodonBoostedStatusVoiceProfile) => "MastodonBoostedStatus",
                    var c when c == typeof(VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile) =>
                        "MastodonBoostedSensitiveStatus",
                    var c when c == typeof(VoiceProfile.MastodonFollowNotificationVoiceProfile) =>
                        "MastodonFollowNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MastodonFollowRequestNotificationVoiceProfile) =>
                        "MastodonFollowRequestNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MastodonMentionNotificationVoiceProfile) =>
                        "MastodonMentionNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MastodonSensitiveMentionNotificationVoiceProfile) =>
                        "MastodonSensitiveMentionNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MastodonReblogNotificationVoiceProfile) =>
                        "MastodonReblogNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MastodonSensitiveReblogNotificationVoiceProfile) =>
                        "MastodonSensitiveReblogNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MastodonFavoriteNotificationVoiceProfile) =>
                        "MastodonFavoriteNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MastodonSensitiveFavoriteNotificationVoiceProfile) =>
                        "MastodonSensitiveFavoriteNotificationVoiceProfile",
                    _ => throw new ArgumentException()
                };

                if (_profiles.TryGetValue((accountIdentifier, type), out var p))
                {
                    return p;
                }

                VoiceProfile profile = type switch
                {
                    "MastodonStatus" => new VoiceProfile.MastodonStatusVoiceProfile(accountIdentifier),
                    "MastodonSensitiveStatus" =>
                        new VoiceProfile.MastodonSensitiveStatusVoiceProfile(accountIdentifier),
                    "MastodonBoostedStatus" => new VoiceProfile.MastodonBoostedStatusVoiceProfile(accountIdentifier),
                    "MastodonBoostedSensitiveStatus" => new VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile(
                        accountIdentifier),
                    "MastodonFollowNotificationVoiceProfile" => new VoiceProfile.MastodonFollowNotificationVoiceProfile(
                        accountIdentifier),
                    "MastodonFollowRequestNotificationVoiceProfile" =>
                        new VoiceProfile.MastodonFollowRequestNotificationVoiceProfile(accountIdentifier),
                    "MastodonMentionNotificationVoiceProfile" =>
                        new VoiceProfile.MastodonMentionNotificationVoiceProfile(accountIdentifier),
                    "MastodonSensitiveMentionNotificationVoiceProfile" => new
                        VoiceProfile.MastodonSensitiveMentionNotificationVoiceProfile(accountIdentifier),
                    "MastodonReblogNotificationVoiceProfile" => new VoiceProfile.MastodonReblogNotificationVoiceProfile(
                        accountIdentifier),
                    "MastodonSensitiveReblogNotificationVoiceProfile" =>
                        new VoiceProfile.MastodonSensitiveReblogNotificationVoiceProfile(accountIdentifier),
                    "MastodonFavoriteNotificationVoiceProfile" =>
                        new VoiceProfile.MastodonFavoriteNotificationVoiceProfile(accountIdentifier),
                    "MastodonSensitiveFavoriteNotificationVoiceProfile" => new VoiceProfile.MastodonSensitiveFavoriteNotificationVoiceProfile(accountIdentifier),
                    _ => throw new ArgumentException()
                };
                _profiles.Add((accountIdentifier, type), profile);

                var data = await _storage.FindVoiceProfile(accountIdentifier.Value, type, cancellationToken);
                if (data is null)
                {
                    return profile;
                }

                profile.Update(data.Volume, data.Speed, data.Tone, data.Alpha, data.ToneScale, data.ComponentNormal,
                    data.ComponentHappy, data.ComponentAnger, data.ComponentSorrow, data.ComponentCalmness);
                return profile;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<IEnumerable<VoiceProfile>> GetVoiceProfiles(AccountIdentifier accountIdentifier,
            CancellationToken cancellationToken)
        {
            try
            {
                await _semaphore.WaitAsync(cancellationToken);

                var data = await _storage.GetVoiceProfiles(accountIdentifier.Value, cancellationToken);
                return data.Select(d =>
                    {
                        if (_profiles.TryGetValue((accountIdentifier, d.Type), out var p))
                        {
                            return p;
                        }

                        VoiceProfile profile = d.Type switch
                        {
                            "MastodonStatus" => new VoiceProfile.MastodonStatusVoiceProfile(accountIdentifier),
                            "MastodonSensitiveStatus" =>
                                new VoiceProfile.MastodonSensitiveStatusVoiceProfile(accountIdentifier),
                            "MastodonBoostedStatus" => new VoiceProfile.MastodonBoostedStatusVoiceProfile(accountIdentifier),
                            "MastodonBoostedSensitiveStatus" => new VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile(
                                accountIdentifier),
                            "MastodonFollowNotificationVoiceProfile" => new VoiceProfile.MastodonFollowNotificationVoiceProfile(
                                accountIdentifier),
                            "MastodonFollowRequestNotificationVoiceProfile" =>
                                new VoiceProfile.MastodonFollowRequestNotificationVoiceProfile(accountIdentifier),
                            "MastodonMentionNotificationVoiceProfile" =>
                                new VoiceProfile.MastodonMentionNotificationVoiceProfile(accountIdentifier),
                            "MastodonSensitiveMentionNotificationVoiceProfile" => new
                                VoiceProfile.MastodonSensitiveMentionNotificationVoiceProfile(accountIdentifier),
                            "MastodonReblogNotificationVoiceProfile" => new VoiceProfile.MastodonReblogNotificationVoiceProfile(
                                accountIdentifier),
                            "MastodonSensitiveReblogNotificationVoiceProfile" =>
                                new VoiceProfile.MastodonSensitiveReblogNotificationVoiceProfile(accountIdentifier),
                            "MastodonFavoriteNotificationVoiceProfile" =>
                                new VoiceProfile.MastodonFavoriteNotificationVoiceProfile(accountIdentifier),
                            "MastodonSensitiveFavoriteNotificationVoiceProfile" => new VoiceProfile.MastodonSensitiveFavoriteNotificationVoiceProfile(accountIdentifier),
                            _ => throw new ArgumentException()
                        };
                        _profiles.Add((accountIdentifier, d.Type), profile);

                        profile.Update(d.Volume, d.Speed, d.Tone, d.Alpha, d.ToneScale, d.ComponentNormal,
                            d.ComponentHappy,
                            d.ComponentAnger, d.ComponentSorrow, d.ComponentCalmness);
                        return profile;
                    })
                    .ToList();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task SaveVoiceProfile(VoiceProfile profile, CancellationToken cancellationToken)
        {
            try
            {
                await _semaphore.WaitAsync(cancellationToken);

                var type = profile switch
                {
                    VoiceProfile.MastodonStatusVoiceProfile => "MastodonStatus",
                    VoiceProfile.MastodonSensitiveStatusVoiceProfile => "MastodonSensitiveStatus",
                    VoiceProfile.MastodonBoostedStatusVoiceProfile => "MastodonBoostedStatus",
                    VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile => "MastodonBoostedSensitiveStatus",
                    VoiceProfile.MastodonFollowNotificationVoiceProfile => "MastodonFollowNotificationVoiceProfile",
                    VoiceProfile.MastodonFollowRequestNotificationVoiceProfile => "MastodonFollowRequestNotificationVoiceProfile",
                    VoiceProfile.MastodonMentionNotificationVoiceProfile => "MastodonMentionNotificationVoiceProfile",
                    VoiceProfile.MastodonSensitiveMentionNotificationVoiceProfile => "MastodonSensitiveMentionNotificationVoiceProfile",
                    VoiceProfile.MastodonReblogNotificationVoiceProfile => "MastodonReblogNotificationVoiceProfile",
                    VoiceProfile.MastodonSensitiveReblogNotificationVoiceProfile => "MastodonSensitiveReblogNotificationVoiceProfile",
                    VoiceProfile.MastodonFavoriteNotificationVoiceProfile => "MastodonFavoriteNotificationVoiceProfile",
                    VoiceProfile.MastodonSensitiveFavoriteNotificationVoiceProfile => "MastodonSensitiveFavoriteNotificationVoiceProfile",
                    _ => throw new ArgumentException()
                };

                var data = new VoiceProfileData(profile.AccountIdentifier.Value, type, profile.Volume, profile.Speed,
                    profile.Tone, profile.Alpha, profile.ToneScale, profile.ComponentNormal, profile.ComponentHappy,
                    profile.ComponentAnger, profile.ComponentSorrow, profile.ComponentCalmness);
                await _storage.SaveVoiceProfile(data, cancellationToken);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
