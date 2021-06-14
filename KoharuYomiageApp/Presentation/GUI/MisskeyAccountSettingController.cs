using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.EditVoiceProfile;
using KoharuYomiageApp.UseCase.EditVoiceProfile.DataObjects;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MisskeyAccountSettingController
    {
        readonly IVoiceProfileUpdater _updater;

        public MisskeyAccountSettingController(IVoiceProfileUpdater updater)
        {
            _updater = updater;
        }

        public async Task SetVoiceProfile(string username, string instance, MisskeyVoiceProfileType type, MisskeyVoiceProfileData data, CancellationToken cancellationToken)
        {
            var ty = type switch
            {
                MisskeyVoiceProfileType.Note => VoiceProfileType.MisskeyNote,
                MisskeyVoiceProfileType.SensitiveNote => VoiceProfileType.MisskeySensitiveNote,
                MisskeyVoiceProfileType.Renote => VoiceProfileType.MisskeyRenote,
                MisskeyVoiceProfileType.SensitiveRenote => VoiceProfileType.MisskeySensitiveRenote,
                MisskeyVoiceProfileType.ReactionNotification => VoiceProfileType.MisskeyReactionNotification,
                MisskeyVoiceProfileType.SensitiveReactionNotification => VoiceProfileType.MisskeySensitiveReactionNotification,
                MisskeyVoiceProfileType.ReplyNotification => VoiceProfileType.MisskeyReplyNotification,
                MisskeyVoiceProfileType.SensitiveReplyNotification => VoiceProfileType.MisskeySensitiveReplyNotification,
                MisskeyVoiceProfileType.RenoteNotification => VoiceProfileType.MisskeyRenoteNotification,
                MisskeyVoiceProfileType.SensitiveRenoteNotification => VoiceProfileType.MisskeySensitiveRenoteNotification,
                MisskeyVoiceProfileType.QuoteNotification => VoiceProfileType.MisskeyQuoteNotification,
                MisskeyVoiceProfileType.SensitiveQuoteNotification => VoiceProfileType.MisskeySensitiveQuoteNotification,
                MisskeyVoiceProfileType.MentionNotification => VoiceProfileType.MisskeyMentionNotification,
                MisskeyVoiceProfileType.SensitiveMentionNotification => VoiceProfileType.MisskeySensitiveMentionNotification,
                MisskeyVoiceProfileType.FollowNotification => VoiceProfileType.MisskeyFollowNotification,
                MisskeyVoiceProfileType.FollowRequestAcceptedNotification => VoiceProfileType.MisskeyFollowRequestAcceptedNotification,
                MisskeyVoiceProfileType.ReceiveFollowRequestNotification => VoiceProfileType.MisskeyReceiveFollowRequestNotification,
                _ => throw new InvalidProgramException(),
            };
            await _updater.SetVoiceProfile(username, instance, ty, new VoiceProfileData(data.Volume, data.Speed, data.Tone,
                data.Alpha, data.ToneScale, data.ComponentNormal, data.ComponentHappy, data.ComponentAnger,
                data.ComponentSorrow, data.ComponentCalmness), cancellationToken);
        }

        public async Task<MisskeyVoiceProfileData> GetVoiceProfile(string username, string instance,
            MisskeyVoiceProfileType type, CancellationToken cancellationToken)
        {
            var ty = type switch
            {
                MisskeyVoiceProfileType.Note => VoiceProfileType.MisskeyNote,
                MisskeyVoiceProfileType.SensitiveNote => VoiceProfileType.MisskeySensitiveNote,
                MisskeyVoiceProfileType.Renote => VoiceProfileType.MisskeyRenote,
                MisskeyVoiceProfileType.SensitiveRenote => VoiceProfileType.MisskeySensitiveRenote,
                MisskeyVoiceProfileType.ReactionNotification => VoiceProfileType.MisskeyReactionNotification,
                MisskeyVoiceProfileType.SensitiveReactionNotification => VoiceProfileType.MisskeySensitiveReactionNotification,
                MisskeyVoiceProfileType.ReplyNotification => VoiceProfileType.MisskeyReplyNotification,
                MisskeyVoiceProfileType.SensitiveReplyNotification => VoiceProfileType.MisskeySensitiveReplyNotification,
                MisskeyVoiceProfileType.RenoteNotification => VoiceProfileType.MisskeyRenoteNotification,
                MisskeyVoiceProfileType.SensitiveRenoteNotification => VoiceProfileType.MisskeySensitiveRenoteNotification,
                MisskeyVoiceProfileType.QuoteNotification => VoiceProfileType.MisskeyQuoteNotification,
                MisskeyVoiceProfileType.SensitiveQuoteNotification => VoiceProfileType.MisskeySensitiveQuoteNotification,
                MisskeyVoiceProfileType.MentionNotification => VoiceProfileType.MisskeyMentionNotification,
                MisskeyVoiceProfileType.SensitiveMentionNotification => VoiceProfileType.MisskeySensitiveMentionNotification,
                MisskeyVoiceProfileType.FollowNotification => VoiceProfileType.MisskeyFollowNotification,
                MisskeyVoiceProfileType.FollowRequestAcceptedNotification => VoiceProfileType.MisskeyFollowRequestAcceptedNotification,
                MisskeyVoiceProfileType.ReceiveFollowRequestNotification => VoiceProfileType.MisskeyReceiveFollowRequestNotification,
                _ => throw new InvalidProgramException(),
            };
            var data = await _updater.GetVoiceProfile(username, instance, ty, cancellationToken);
            return new MisskeyVoiceProfileData(data.Volume, data.Speed, data.Tone, data.Alpha, data.ToneScale,
                data.ComponentNormal, data.ComponentHappy, data.ComponentAnger, data.ComponentSorrow,
                data.ComponentCalmness);
        }

        public async Task PlaySampleVoice(string username, string instance, MisskeyVoiceProfileType type,
            string sampleText, CancellationToken cancellationToken)
        {
            var ty = type switch
            {
                MisskeyVoiceProfileType.Note => VoiceProfileType.MisskeyNote,
                MisskeyVoiceProfileType.SensitiveNote => VoiceProfileType.MisskeySensitiveNote,
                MisskeyVoiceProfileType.Renote => VoiceProfileType.MisskeyRenote,
                MisskeyVoiceProfileType.SensitiveRenote => VoiceProfileType.MisskeySensitiveRenote,
                MisskeyVoiceProfileType.ReactionNotification => VoiceProfileType.MisskeyReactionNotification,
                MisskeyVoiceProfileType.SensitiveReactionNotification => VoiceProfileType.MisskeySensitiveReactionNotification,
                MisskeyVoiceProfileType.ReplyNotification => VoiceProfileType.MisskeyReplyNotification,
                MisskeyVoiceProfileType.SensitiveReplyNotification => VoiceProfileType.MisskeySensitiveReplyNotification,
                MisskeyVoiceProfileType.RenoteNotification => VoiceProfileType.MisskeyRenoteNotification,
                MisskeyVoiceProfileType.SensitiveRenoteNotification => VoiceProfileType.MisskeySensitiveRenoteNotification,
                MisskeyVoiceProfileType.QuoteNotification => VoiceProfileType.MisskeyQuoteNotification,
                MisskeyVoiceProfileType.SensitiveQuoteNotification => VoiceProfileType.MisskeySensitiveQuoteNotification,
                MisskeyVoiceProfileType.MentionNotification => VoiceProfileType.MisskeyMentionNotification,
                MisskeyVoiceProfileType.SensitiveMentionNotification => VoiceProfileType.MisskeySensitiveMentionNotification,
                MisskeyVoiceProfileType.FollowNotification => VoiceProfileType.MisskeyFollowNotification,
                MisskeyVoiceProfileType.FollowRequestAcceptedNotification => VoiceProfileType.MisskeyFollowRequestAcceptedNotification,
                MisskeyVoiceProfileType.ReceiveFollowRequestNotification => VoiceProfileType.MisskeyReceiveFollowRequestNotification,
                _ => throw new InvalidProgramException(),
            };
            await _updater.PlaySampleVoice(username, instance, ty, sampleText, cancellationToken);
        }

        public enum MisskeyVoiceProfileType
        {
            Note,
            SensitiveNote,
            Renote,
            SensitiveRenote,
            ReactionNotification,
            SensitiveReactionNotification,
            ReplyNotification,
            SensitiveReplyNotification,
            RenoteNotification,
            SensitiveRenoteNotification,
            QuoteNotification,
            SensitiveQuoteNotification,
            MentionNotification,
            SensitiveMentionNotification,
            FollowNotification,
            FollowRequestAcceptedNotification,
            ReceiveFollowRequestNotification,
        }

        public record MisskeyVoiceProfileData(double Volume, double Speed, double Tone, double Alpha, double ToneScale,
            double ComponentNormal, double ComponentHappy, double ComponentAnger, double ComponentSorrow,
            double ComponentCalmness);
    }
}
