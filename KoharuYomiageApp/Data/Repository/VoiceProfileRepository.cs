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
                    var c when c == typeof(VoiceProfile.MastodonStatusVoiceProfile) => "MastodonStatusVoiceProfile",
                    var c when c == typeof(VoiceProfile.MastodonSensitiveStatusVoiceProfile) =>
                        "MastodonSensitiveStatusVoiceProfile",
                    var c when c == typeof(VoiceProfile.MastodonBoostedStatusVoiceProfile) =>
                        "MastodonBoostedStatusVoiceProfile",
                    var c when c == typeof(VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile) =>
                        "MastodonBoostedSensitiveStatusVoiceProfile",
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

                    var c when c == typeof(VoiceProfile.MisskeyNoteVoiceProfile) => "MisskeyNoteVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeySensitiveNoteVoiceProfile) =>
                        "MisskeySensitiveNoteVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeyRenoteVoiceProfile) => "MisskeyRenoteVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeySensitiveRenoteVoiceProfile) =>
                        "MisskeySensitiveRenoteVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeyReactionNotificationVoiceProfile) =>
                        "MisskeyReactionNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeySensitiveReactionNotificationVoiceProfile) =>
                        "MisskeySensitiveReactionNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeyReplyNotificationVoiceProfile) =>
                        "MisskeyReplyNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeySensitiveReplyNotificationVoiceProfile) =>
                        "MisskeySensitiveReplyNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeyRenoteNotificationVoiceProfile) =>
                        "MisskeyRenoteNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeySensitiveRenoteNotificationVoiceProfile) =>
                        "MisskeySensitiveRenoteNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeyQuoteNotificationVoiceProfile) =>
                        "MisskeyQuoteNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeySensitiveQuoteNotificationVoiceProfile) =>
                        "MisskeySensitiveQuoteNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeyMentionNotificationVoiceProfile) =>
                        "MisskeyMentionNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeySensitiveMentionNotificationVoiceProfile) =>
                        "MisskeySensitiveMentionNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeyFollowNotificationVoiceProfile) =>
                        "MisskeyFollowNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeyFollowRequestAcceptedNotificationVoiceProfile) =>
                        "MisskeyFollowRequestAcceptedNotificationVoiceProfile",
                    var c when c == typeof(VoiceProfile.MisskeyReceiveFollowRequestNotificationVoiceProfile) =>
                        "MisskeyReceiveFollowRequestNotificationVoiceProfile",

                    _ => throw new ArgumentException()
                };

                if (_profiles.TryGetValue((accountIdentifier, type), out var p))
                {
                    return p;
                }

                VoiceProfile profile = type switch
                {
                    "MastodonStatusVoiceProfile" => new VoiceProfile.MastodonStatusVoiceProfile(accountIdentifier),
                    "MastodonSensitiveStatusVoiceProfile" =>
                        new VoiceProfile.MastodonSensitiveStatusVoiceProfile(accountIdentifier),
                    "MastodonBoostedStatusVoiceProfile" => new VoiceProfile.MastodonBoostedStatusVoiceProfile(
                        accountIdentifier),
                    "MastodonBoostedSensitiveStatusVoiceProfile" => new
                        VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile(
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
                    "MastodonSensitiveFavoriteNotificationVoiceProfile" => new
                        VoiceProfile.MastodonSensitiveFavoriteNotificationVoiceProfile(accountIdentifier),

                    "MisskeyNoteVoiceProfile" => new VoiceProfile.MisskeyNoteVoiceProfile(accountIdentifier),
                    "MisskeySensitiveNoteVoiceProfile" => new VoiceProfile.MisskeySensitiveNoteVoiceProfile(
                        accountIdentifier),
                    "MisskeyRenoteVoiceProfile" => new VoiceProfile.MisskeyRenoteVoiceProfile(accountIdentifier),
                    "MisskeySensitiveRenoteVoiceProfile" => new VoiceProfile.MisskeySensitiveRenoteVoiceProfile(
                        accountIdentifier),
                    "MisskeyReactionNotificationVoiceProfile" =>
                        new VoiceProfile.MisskeyReactionNotificationVoiceProfile(accountIdentifier),
                    "MisskeySensitiveReactionNotificationVoiceProfile" => new
                        VoiceProfile.MisskeySensitiveReactionNotificationVoiceProfile(accountIdentifier),
                    "MisskeyReplyNotificationVoiceProfile" => new VoiceProfile.MisskeyReplyNotificationVoiceProfile(
                        accountIdentifier),
                    "MisskeySensitiveReplyNotificationVoiceProfile" =>
                        new VoiceProfile.MisskeySensitiveReplyNotificationVoiceProfile(accountIdentifier),
                    "MisskeyRenoteNotificationVoiceProfile" => new VoiceProfile.MisskeyRenoteNotificationVoiceProfile(
                        accountIdentifier),
                    "MisskeySensitiveRenoteNotificationVoiceProfile" =>
                        new VoiceProfile.MisskeySensitiveRenoteNotificationVoiceProfile(accountIdentifier),
                    "MisskeyQuoteNotificationVoiceProfile" => new VoiceProfile.MisskeyQuoteNotificationVoiceProfile(
                        accountIdentifier),
                    "MisskeySensitiveQuoteNotificationVoiceProfile" =>
                        new VoiceProfile.MisskeySensitiveQuoteNotificationVoiceProfile(accountIdentifier),
                    "MisskeyMentionNotificationVoiceProfile" => new VoiceProfile.MisskeyMentionNotificationVoiceProfile(
                        accountIdentifier),
                    "MisskeySensitiveMentionNotificationVoiceProfile" =>
                        new VoiceProfile.MisskeySensitiveMentionNotificationVoiceProfile(accountIdentifier),
                    "MisskeyFollowNotificationVoiceProfile" => new VoiceProfile.MisskeyFollowNotificationVoiceProfile(
                        accountIdentifier),
                    "MisskeyFollowRequestAcceptedNotificationVoiceProfile" => new
                        VoiceProfile.MisskeyFollowRequestAcceptedNotificationVoiceProfile(accountIdentifier),
                    "MisskeyReceiveFollowRequestNotificationVoiceProfile" => new
                        VoiceProfile.MisskeyReceiveFollowRequestNotificationVoiceProfile(accountIdentifier),

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
                            "MastodonStatusVoiceProfile" => new VoiceProfile.MastodonStatusVoiceProfile(
                                accountIdentifier),
                            "MastodonSensitiveStatusVoiceProfile" =>
                                new VoiceProfile.MastodonSensitiveStatusVoiceProfile(accountIdentifier),
                            "MastodonBoostedStatusVoiceProfile" => new VoiceProfile.MastodonBoostedStatusVoiceProfile(
                                accountIdentifier),
                            "MastodonBoostedSensitiveStatusVoiceProfile" => new
                                VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile(
                                    accountIdentifier),
                            "MastodonFollowNotificationVoiceProfile" => new
                                VoiceProfile.MastodonFollowNotificationVoiceProfile(
                                    accountIdentifier),
                            "MastodonFollowRequestNotificationVoiceProfile" =>
                                new VoiceProfile.MastodonFollowRequestNotificationVoiceProfile(accountIdentifier),
                            "MastodonMentionNotificationVoiceProfile" =>
                                new VoiceProfile.MastodonMentionNotificationVoiceProfile(accountIdentifier),
                            "MastodonSensitiveMentionNotificationVoiceProfile" => new
                                VoiceProfile.MastodonSensitiveMentionNotificationVoiceProfile(accountIdentifier),
                            "MastodonReblogNotificationVoiceProfile" => new
                                VoiceProfile.MastodonReblogNotificationVoiceProfile(
                                    accountIdentifier),
                            "MastodonSensitiveReblogNotificationVoiceProfile" =>
                                new VoiceProfile.MastodonSensitiveReblogNotificationVoiceProfile(accountIdentifier),
                            "MastodonFavoriteNotificationVoiceProfile" =>
                                new VoiceProfile.MastodonFavoriteNotificationVoiceProfile(accountIdentifier),
                            "MastodonSensitiveFavoriteNotificationVoiceProfile" => new
                                VoiceProfile.MastodonSensitiveFavoriteNotificationVoiceProfile(accountIdentifier),

                            "MisskeyNoteVoiceProfile" => new VoiceProfile.MisskeyNoteVoiceProfile(accountIdentifier),
                            "MisskeySensitiveNoteVoiceProfile" => new VoiceProfile.MisskeySensitiveNoteVoiceProfile(
                                accountIdentifier),
                            "MisskeyRenoteVoiceProfile" =>
                                new VoiceProfile.MisskeyRenoteVoiceProfile(accountIdentifier),
                            "MisskeySensitiveRenoteVoiceProfile" => new VoiceProfile.MisskeySensitiveRenoteVoiceProfile(
                                accountIdentifier),
                            "MisskeyReactionNotificationVoiceProfile" =>
                                new VoiceProfile.MisskeyReactionNotificationVoiceProfile(accountIdentifier),
                            "MisskeySensitiveReactionNotificationVoiceProfile" => new
                                VoiceProfile.MisskeySensitiveReactionNotificationVoiceProfile(accountIdentifier),
                            "MisskeyReplyNotificationVoiceProfile" =>
                                new VoiceProfile.MisskeyReplyNotificationVoiceProfile(accountIdentifier),
                            "MisskeySensitiveReplyNotificationVoiceProfile" => new
                                VoiceProfile.MisskeySensitiveReplyNotificationVoiceProfile(accountIdentifier),
                            "MisskeyRenoteNotificationVoiceProfile" =>
                                new VoiceProfile.MisskeyRenoteNotificationVoiceProfile(accountIdentifier),
                            "MisskeySensitiveRenoteNotificationVoiceProfile" => new
                                VoiceProfile.MisskeySensitiveRenoteNotificationVoiceProfile(accountIdentifier),
                            "MisskeyQuoteNotificationVoiceProfile" =>
                                new VoiceProfile.MisskeyQuoteNotificationVoiceProfile(accountIdentifier),
                            "MisskeySensitiveQuoteNotificationVoiceProfile" => new
                                VoiceProfile.MisskeySensitiveQuoteNotificationVoiceProfile(accountIdentifier),
                            "MisskeyMentionNotificationVoiceProfile" =>
                                new VoiceProfile.MisskeyMentionNotificationVoiceProfile(accountIdentifier),
                            "MisskeySensitiveMentionNotificationVoiceProfile" => new
                                VoiceProfile.MisskeySensitiveMentionNotificationVoiceProfile(accountIdentifier),
                            "MisskeyFollowNotificationVoiceProfile" =>
                                new VoiceProfile.MisskeyFollowNotificationVoiceProfile(accountIdentifier),
                            "MisskeyFollowRequestAcceptedNotificationVoiceProfile" => new
                                VoiceProfile.MisskeyFollowRequestAcceptedNotificationVoiceProfile(accountIdentifier),
                            "MisskeyReceiveFollowRequestNotificationVoiceProfile" => new
                                VoiceProfile.MisskeyReceiveFollowRequestNotificationVoiceProfile(accountIdentifier),

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
                    VoiceProfile.MastodonStatusVoiceProfile => "MastodonStatusVoiceProfile",
                    VoiceProfile.MastodonSensitiveStatusVoiceProfile => "MastodonSensitiveStatusVoiceProfile",
                    VoiceProfile.MastodonBoostedStatusVoiceProfile => "MastodonBoostedStatusVoiceProfile",
                    VoiceProfile.MastodonBoostedSensitiveStatusVoiceProfile =>
                        "MastodonBoostedSensitiveStatusVoiceProfile",
                    VoiceProfile.MastodonFollowNotificationVoiceProfile => "MastodonFollowNotificationVoiceProfile",
                    VoiceProfile.MastodonFollowRequestNotificationVoiceProfile =>
                        "MastodonFollowRequestNotificationVoiceProfile",
                    VoiceProfile.MastodonMentionNotificationVoiceProfile => "MastodonMentionNotificationVoiceProfile",
                    VoiceProfile.MastodonSensitiveMentionNotificationVoiceProfile =>
                        "MastodonSensitiveMentionNotificationVoiceProfile",
                    VoiceProfile.MastodonReblogNotificationVoiceProfile => "MastodonReblogNotificationVoiceProfile",
                    VoiceProfile.MastodonSensitiveReblogNotificationVoiceProfile =>
                        "MastodonSensitiveReblogNotificationVoiceProfile",
                    VoiceProfile.MastodonFavoriteNotificationVoiceProfile => "MastodonFavoriteNotificationVoiceProfile",
                    VoiceProfile.MastodonSensitiveFavoriteNotificationVoiceProfile =>
                        "MastodonSensitiveFavoriteNotificationVoiceProfile",

                    VoiceProfile.MisskeyNoteVoiceProfile => "MisskeyNoteVoiceProfile",
                    VoiceProfile.MisskeySensitiveNoteVoiceProfile => "MisskeySensitiveNoteVoiceProfile",
                    VoiceProfile.MisskeyRenoteVoiceProfile => "MisskeyRenoteVoiceProfile",
                    VoiceProfile.MisskeySensitiveRenoteVoiceProfile => "MisskeySensitiveRenoteVoiceProfile",
                    VoiceProfile.MisskeyReactionNotificationVoiceProfile => "MisskeyReactionNotificationVoiceProfile",
                    VoiceProfile.MisskeySensitiveReactionNotificationVoiceProfile =>
                        "MisskeySensitiveReactionNotificationVoiceProfile",
                    VoiceProfile.MisskeyReplyNotificationVoiceProfile => "MisskeyReplyNotificationVoiceProfile",
                    VoiceProfile.MisskeySensitiveReplyNotificationVoiceProfile =>
                        "MisskeySensitiveReplyNotificationVoiceProfile",
                    VoiceProfile.MisskeyRenoteNotificationVoiceProfile => "MisskeyRenoteNotificationVoiceProfile",
                    VoiceProfile.MisskeySensitiveRenoteNotificationVoiceProfile =>
                        "MisskeySensitiveRenoteNotificationVoiceProfile",
                    VoiceProfile.MisskeyQuoteNotificationVoiceProfile => "MisskeyQuoteNotificationVoiceProfile",
                    VoiceProfile.MisskeySensitiveQuoteNotificationVoiceProfile =>
                        "MisskeySensitiveQuoteNotificationVoiceProfile",
                    VoiceProfile.MisskeyMentionNotificationVoiceProfile => "MisskeyMentionNotificationVoiceProfile",
                    VoiceProfile.MisskeySensitiveMentionNotificationVoiceProfile =>
                        "MisskeySensitiveMentionNotificationVoiceProfile",
                    VoiceProfile.MisskeyFollowNotificationVoiceProfile => "MisskeyFollowNotificationVoiceProfile",
                    VoiceProfile.MisskeyFollowRequestAcceptedNotificationVoiceProfile =>
                        "MisskeyFollowRequestAcceptedNotificationVoiceProfile",
                    VoiceProfile.MisskeyReceiveFollowRequestNotificationVoiceProfile =>
                        "MisskeyReceiveFollowRequestNotificationVoiceProfile",

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
