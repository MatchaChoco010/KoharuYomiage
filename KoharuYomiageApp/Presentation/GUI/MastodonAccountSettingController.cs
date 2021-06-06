using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.EditVoiceProfile;
using KoharuYomiageApp.UseCase.EditVoiceProfile.DataObjects;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MastodonAccountSettingController
    {
        readonly IVoiceProfileUpdater _updater;

        public MastodonAccountSettingController(IVoiceProfileUpdater updater)
        {
            _updater = updater;
        }

        public async Task SetVoiceProfile(string username, string instance, MastodonVoiceProfileType type, MastodonVoiceProfileData data, CancellationToken cancellationToken)
        {
            var ty = type switch
            {
                MastodonVoiceProfileType.Status => VoiceProfileType.MastodonStatus,
                MastodonVoiceProfileType.SensitiveStatus => VoiceProfileType.MastodonSensitiveStatus,
                MastodonVoiceProfileType.BoostedStatus => VoiceProfileType.MastodonBoostedStatus,
                MastodonVoiceProfileType.BoostedSensitiveStatus => VoiceProfileType.MastodonBoostedSensitiveStatus,
                MastodonVoiceProfileType.FollowNotification => VoiceProfileType.MastodonFollowNotification,
                MastodonVoiceProfileType.FollowRequestNotification => VoiceProfileType.MastodonFollowRequestNotification,
                MastodonVoiceProfileType.MentionNotification => VoiceProfileType.MastodonMentionNotification,
                MastodonVoiceProfileType.SensitiveMentionNotification => VoiceProfileType.MastodonSensitiveMentionNotification,
                MastodonVoiceProfileType.ReblogNotification => VoiceProfileType.MastodonReblogNotification,
                MastodonVoiceProfileType.SensitiveReblogNotification => VoiceProfileType.MastodonSensitiveReblogNotification,
                MastodonVoiceProfileType.FavoriteNotification => VoiceProfileType.MastodonFavoriteNotification,
                MastodonVoiceProfileType.SensitiveFavoriteNotification => VoiceProfileType.MastodonSensitiveFavoriteNotification,
                _ => throw new InvalidProgramException(),
            };
            await _updater.SetVoiceProfile(username, instance, ty, new VoiceProfileData(data.Volume, data.Speed, data.Tone,
                data.Alpha, data.ToneScale, data.ComponentNormal, data.ComponentHappy, data.ComponentAnger,
                data.ComponentSorrow, data.ComponentCalmness), cancellationToken);
        }

        public async Task<MastodonVoiceProfileData> GetVoiceProfile(string username, string instance,
            MastodonVoiceProfileType type, CancellationToken cancellationToken)
        {
            var ty = type switch
            {
                MastodonVoiceProfileType.Status => VoiceProfileType.MastodonStatus,
                MastodonVoiceProfileType.SensitiveStatus => VoiceProfileType.MastodonSensitiveStatus,
                MastodonVoiceProfileType.BoostedStatus => VoiceProfileType.MastodonBoostedStatus,
                MastodonVoiceProfileType.BoostedSensitiveStatus => VoiceProfileType.MastodonBoostedSensitiveStatus,
                MastodonVoiceProfileType.FollowNotification => VoiceProfileType.MastodonFollowNotification,
                MastodonVoiceProfileType.FollowRequestNotification => VoiceProfileType.MastodonFollowRequestNotification,
                MastodonVoiceProfileType.MentionNotification => VoiceProfileType.MastodonMentionNotification,
                MastodonVoiceProfileType.SensitiveMentionNotification => VoiceProfileType.MastodonSensitiveMentionNotification,
                MastodonVoiceProfileType.ReblogNotification => VoiceProfileType.MastodonReblogNotification,
                MastodonVoiceProfileType.SensitiveReblogNotification => VoiceProfileType.MastodonSensitiveReblogNotification,
                MastodonVoiceProfileType.FavoriteNotification => VoiceProfileType.MastodonFavoriteNotification,
                MastodonVoiceProfileType.SensitiveFavoriteNotification => VoiceProfileType.MastodonSensitiveFavoriteNotification,
                _ => throw new InvalidProgramException(),
            };
            var data = await _updater.GetVoiceProfile(username, instance, ty, cancellationToken);
            return new MastodonVoiceProfileData(data.Volume, data.Speed, data.Tone, data.Alpha, data.ToneScale,
                data.ComponentNormal, data.ComponentHappy, data.ComponentAnger, data.ComponentSorrow,
                data.ComponentCalmness);
        }

        public async Task PlaySampleVoice(string username, string instance, MastodonVoiceProfileType type,
            string sampleText, CancellationToken cancellationToken)
        {
            var ty = type switch
            {
                MastodonVoiceProfileType.Status => VoiceProfileType.MastodonStatus,
                MastodonVoiceProfileType.SensitiveStatus => VoiceProfileType.MastodonSensitiveStatus,
                MastodonVoiceProfileType.BoostedStatus => VoiceProfileType.MastodonBoostedStatus,
                MastodonVoiceProfileType.BoostedSensitiveStatus => VoiceProfileType.MastodonBoostedSensitiveStatus,
                MastodonVoiceProfileType.FollowNotification => VoiceProfileType.MastodonFollowNotification,
                MastodonVoiceProfileType.FollowRequestNotification => VoiceProfileType.MastodonFollowRequestNotification,
                MastodonVoiceProfileType.MentionNotification => VoiceProfileType.MastodonMentionNotification,
                MastodonVoiceProfileType.SensitiveMentionNotification => VoiceProfileType.MastodonSensitiveMentionNotification,
                MastodonVoiceProfileType.ReblogNotification => VoiceProfileType.MastodonReblogNotification,
                MastodonVoiceProfileType.SensitiveReblogNotification => VoiceProfileType.MastodonSensitiveReblogNotification,
                MastodonVoiceProfileType.FavoriteNotification => VoiceProfileType.MastodonFavoriteNotification,
                MastodonVoiceProfileType.SensitiveFavoriteNotification => VoiceProfileType.MastodonSensitiveFavoriteNotification,
                _ => throw new InvalidProgramException(),
            };
            await _updater.PlaySampleVoice(username, instance, ty, sampleText, cancellationToken);
        }

        public enum MastodonVoiceProfileType
        {
            Status,
            SensitiveStatus,
            BoostedStatus,
            BoostedSensitiveStatus,
            FollowNotification,
            FollowRequestNotification,
            MentionNotification,
            SensitiveMentionNotification,
            ReblogNotification,
            SensitiveReblogNotification,
            FavoriteNotification,
            SensitiveFavoriteNotification,
        }

        public record MastodonVoiceProfileData(double Volume, double Speed, double Tone, double Alpha, double ToneScale,
            double ComponentNormal, double ComponentHappy, double ComponentAnger, double ComponentSorrow,
            double ComponentCalmness);
    }
}
