using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Presentation.Misskey;
using KoharuYomiageApp.Presentation.Misskey.DataObjects;
using MisskeyApi;
using MisskeyApi.Payloads;
using MisskeyApi.Payloads.Entities;

namespace KoharuYomiageApp.Infrastructures.Misskey
{
    public class MisskeyClient : IMakeMisskeyConnection, IMisskeyRegisterClient, IMisskeyGetAuthorizeUrl,
        IMisskeyGetAccessToken
    {
        readonly MisskeyController _misskeyController;

        public MisskeyClient(MisskeyController misskeyController)
        {
            _misskeyController = misskeyController;
        }

        public async Task<string> RegisterClient(string instance, CancellationToken cancellationToken = new())
        {
            var secrtet = await Api.RegisterApp(instance, cancellationToken);
            return secrtet.Value;
        }

        public async Task<(string, Uri)> GetAuthorizeUri(string hostName, string secret,
            CancellationToken cancellationToken = new())
        {
            var (sessionToken, authrizeUrl) =
                await Api.GetAuthorizeUri(hostName, new Secret(secret), cancellationToken);
            return (sessionToken.Value, authrizeUrl);
        }

        public async Task<(string, (string, string, Uri))> GetAccessToken(string instance, string secret,
            string sessionToken,
            CancellationToken cancellationToken = new())
        {
            var (accessToken, user) = await Api.GetAccessToken(instance, new Secret(secret),
                new SessionToken(sessionToken), cancellationToken);
            return (accessToken.Value, (user.username, user.name, user.avatarUrl));
        }

        public IDisposable MakeConnection(string username, string instance, string accessToken)
        {
            return Api.GetUserStreamingObservable(instance, new AccessToken(accessToken))
                .Subscribe(item =>
                {
                    switch (item)
                    {
                        case UserStreamPayload.Note(var note):
                            switch (note)
                            {
                                case { renote: { cw: not null } }:
                                    _misskeyController.AddSensitiveRenote(new MisskeySensitiveRenoteInputData(
                                        username, instance, note.renote.user.name, note.renote.user.username,
                                        note.user.name, note.user.username, note.renote.text ?? "", note.renote.cw));
                                    break;
                                case { renote: not null }:
                                    _misskeyController.AddRenote(new MisskeyRenoteInputData(
                                        username, instance, note.renote.user.name, note.renote.user.username,
                                        note.user.name, note.user.username, note.renote.text ?? ""));
                                    break;
                                case { cw: not null }:
                                    _misskeyController.AddSensitiveNote(new MisskeySensitiveNoteInputData(
                                        username, instance, note.user.name, note.user.username, note.text ?? "",
                                        note.cw));
                                    break;
                                default:
                                    _misskeyController.AddNote(new MisskeyNoteInputData(
                                        username, instance, note.user.name, note.user.username, note.text ?? ""));
                                    break;
                            }

                            break;
                        case UserStreamPayload.Notification(var notification):
                            switch (notification)
                            {
                                case Notification.Reaction
                                {
                                    reaction: var reaction, user: var user,
                                    note: { cw: not null and var cw } and var note
                                }:
                                    _misskeyController.AddSensitiveReactionNotification(
                                        new MisskeySensitiveReactionNotificationInputData(
                                            username, instance, note.user.name, note.user.username, note.text ?? "",
                                            cw, user.name, user.username, reaction));
                                    break;
                                case Notification.Reaction { reaction: var reaction, user: var user, note: var note }:
                                    _misskeyController.AddReactionNotification(new MisskeyReactionNotificationInputData(
                                        username, instance, note.user.name, note.user.username, note.text ?? "",
                                        user.name, user.username, reaction));
                                    break;
                                case Notification.Reply
                                {
                                    user: var user, note: { cw: not null and var cw } and var note
                                }:
                                    _misskeyController.AddSensitiveReplyNotification(
                                        new MisskeySensitiveReplyNotificationInputData(
                                            username, instance, user.name, user.username, note.text ?? "", cw));
                                    break;
                                case Notification.Reply { user: var user, note: var note }:
                                    _misskeyController.AddReplyNotification(new MisskeyReplyNotificationInputData(
                                        username, instance, user.name, user.username, note.text ?? ""));
                                    break;
                                case Notification.Renote
                                {
                                    user: var user, note: { cw: not null and var cw } and var note
                                }:
                                    _misskeyController.AddSensitiveRenoteNotification(
                                        new MisskeySensitiveRenoteNotificationInputData(
                                            username, instance, note.user.name, note.user.username, user.name,
                                            user.username,
                                            note.text ?? "", cw));
                                    break;
                                case Notification.Renote { user: var user, note: var note }:
                                    _misskeyController.AddRenoteNotification(new MisskeyRenoteNotificationInputData(
                                        username, instance, note.user.name, note.user.username, user.name,
                                        user.username,
                                        note.text ?? ""));
                                    break;
                                case Notification.Quote
                                {
                                    user: var user, note: { cw: not null and var cw } and var note
                                }:
                                    _misskeyController.AddSensitiveQuoteNotification(
                                        new MisskeySensitiveQuoteNotificationInputData(
                                            username, instance, user.name, user.username,
                                            note.text ?? "", cw));
                                    break;
                                case Notification.Quote { user: var user, note: var note }:
                                    _misskeyController.AddQuoteNotification(new MisskeyQuoteNotificationInputData(
                                        username, instance, user.name, user.username, note.text ?? ""));
                                    break;
                                case Notification.Mention
                                {
                                    user: var user, note: { cw: not null and var cw } and var note
                                }:
                                    _misskeyController.AddSensitiveMentionNotification(
                                        new MisskeySensitiveMentionNotificationInputData(
                                            username, instance, user.name, user.username, note.text ?? "", cw));
                                    break;
                                case Notification.Mention { user: var user, note: var note }:
                                    _misskeyController.AddMentionNotification(new MisskeyMentionNotificationInputData(
                                        username, instance, user.name, user.username, note.text ?? ""));
                                    break;
                                case Notification.Follow { user: var user }:
                                    _misskeyController.AddFollowNotification(new MisskeyFollowNotificationInputData(
                                        username, instance, user.name, user.username));
                                    break;
                                case Notification.FollowRequestAccepted { user: var user }:
                                    _misskeyController.AddFollowRequestAcceptNotification(
                                        new MisskeyFollowRequestAcceptNotificationInputData(
                                            username, instance, user.name, user.username));
                                    break;
                                case Notification.ReceiveFollowRequest { user: var user }:
                                    _misskeyController.AddReceiveFollowRequestNotification(
                                        new MisskeyReceiveFollowRequestNotificationInputData(
                                            username, instance, user.name, user.username));
                                    break;
                            }

                            break;
                    }
                });
        }
    }
}
