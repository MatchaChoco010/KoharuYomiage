using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Presentation.Mastodon;
using KoharuYomiageApp.Presentation.Mastodon.DataObjects;
using MastodonApi;
using MastodonApi.Payloads;

namespace KoharuYomiageApp.Infrastructures.Mastodon
{
    public class MastodonClient : IMakeMastodonConnection, IMastodonRegisterClient,
        IMastodonAuthorizeAccountWithCode, IMastodonGetAccountInfo
    {
        readonly MastodonController _mastodonController;

        public MastodonClient(MastodonController mastodonController)
        {
            _mastodonController = mastodonController;
        }

        public IDisposable MakeConnection(string username, string instance, string accessToken)
        {
            return Api.GetUserStreamingObservable(instance, new AccessToken(accessToken))
                .SelectMany(item => Observable.StartAsync(async cancellationToken =>
                {
                    switch (item)
                    {
                        case UserStreamPayload.Status(var status):
                            switch (status)
                            {
                                case {sensitive: true, reblog: null}:
                                case {spoiler_text: var spoilerText, reblog: null}
                                    when !string.IsNullOrEmpty(spoilerText):
                                    await _mastodonController.AddMastodonSensitiveStatus(
                                        new MastodonSensitiveStatusInputData(username, instance,
                                            status.account.display_name, status.account.username,
                                            status.spoiler_text, status.content,
                                            status.media_attachments.Select(media => media.description ?? "")), cancellationToken);
                                    break;
                                case {sensitive: false, reblog: null}:
                                    await _mastodonController.AddMastodonStatus(new MastodonStatusInputData(
                                        username, instance, status.account.display_name, status.account.username,
                                        status.content,
                                        status.media_attachments.Select(media => media.description ?? "")), cancellationToken);
                                    break;
                                case {sensitive: true, reblog: not null}:
                                case {spoiler_text: var spoilerText, reblog: not null}
                                    when !string.IsNullOrEmpty(spoilerText):
                                    await _mastodonController.AddMastodonBoostedSensitiveStatus(
                                        new MastodonBoostedSensitiveStatusInputData(username, instance,
                                            status.account.display_name, status.account.username,
                                            status.reblog.account.display_name, status.reblog.account.username,
                                            status.spoiler_text, status.content,
                                            status.media_attachments.Select(media => media.description ?? "")), cancellationToken);
                                    break;
                                case {sensitive: false, reblog: not null}:
                                    await _mastodonController.AddMastodonBoostedStatus(
                                        new MastodonBoostedStatusInputData(username, instance,
                                            status.account.display_name, status.account.username,
                                            status.reblog.account.display_name, status.reblog.account.username,
                                            status.content,
                                            status.media_attachments.Select(media => media.description ?? "")), cancellationToken);
                                    break;
                            }

                            break;
                        case UserStreamPayload.Notification(var notification):
                            switch (notification)
                            {
                                case {type: "follow"}:
                                    await _mastodonController.AddMastodonFollowNotification(
                                        new MastodonFollowNotificationInputData(username, instance,
                                            notification.account.display_name, notification.account.username), cancellationToken);
                                    break;
                                case {type: "follow_request"}:
                                    await _mastodonController.AddMastodonFollowRequestNotification(
                                        new MastodonFollowRequestNotificationInputData(username, instance,
                                            notification.account.display_name, notification.account.username), cancellationToken);
                                    break;
                                case {type: "mention", status: {sensitive: true}}:
                                case {type: "mention", status: {spoiler_text: var spoilerText}} when !string.IsNullOrEmpty(spoilerText):
                                    await _mastodonController.AddMastodonSensitiveMentionNotification(
                                        new MastodonSensitiveMentionNotificationInputData(username, instance,
                                            notification.account.display_name, notification.account.username,
                                            notification.status.spoiler_text, notification.status.content,
                                            notification.status.media_attachments.Select(media => media.description ?? "")), cancellationToken);
                                    break;
                                case {type: "mention", status: not null}:
                                    await _mastodonController.AddMastodonMentionNotification(
                                        new MastodonMentionNotificationInputData(username, instance,
                                            notification.account.display_name, notification.account.username,
                                            notification.status.content,
                                            notification.status.media_attachments.Select(media => media.description ?? "")), cancellationToken);
                                    break;
                                case {type: "reblog", status: {sensitive: true}}:
                                case {type: "reblog", status: {spoiler_text: var spoilerText}} when !string.IsNullOrEmpty(spoilerText):
                                    await _mastodonController.AddMastodonSensitiveReblogNotification(
                                        new MastodonSensitiveReblogNotificationInputData(username, instance,
                                            notification.account.display_name, notification.account.username,
                                            notification.status.spoiler_text, notification.status.content,
                                            notification.status.media_attachments.Select(media => media.description ?? "")), cancellationToken);
                                    break;
                                case {type: "reblog", status: not null}:
                                    await _mastodonController.AddMastodonReblogNotification(
                                        new MastodonReblogNotificationInputData(username, instance,
                                            notification.account.display_name, notification.account.username,
                                            notification.status.content,
                                            notification.status.media_attachments.Select(media => media.description ?? "")), cancellationToken);
                                    break;
                                case {type: "favourite", status: {sensitive: true}}:
                                case {type: "favourite", status: {spoiler_text: var spoilerText}} when !string.IsNullOrEmpty(spoilerText):
                                    await _mastodonController.AddMastodonSensitiveFavoriteNotification(
                                        new MastodonSensitiveFavoriteNotificationInputData(username, instance,
                                            notification.account.display_name, notification.account.username,
                                            notification.status.spoiler_text, notification.status.content,
                                            notification.status.media_attachments.Select(media => media.description ?? "")), cancellationToken);
                                    break;
                                case {type: "favourite", status: not null}:
                                    await _mastodonController.AddMastodonFavoriteNotification(
                                        new MastodonFavoriteNotificationInputData(username, instance,
                                            notification.account.display_name, notification.account.username,
                                            notification.status.content,
                                            notification.status.media_attachments.Select(media => media.description ?? "")), cancellationToken);
                                    break;
                            }
                            break;
                    }
                }))
                .Subscribe();
        }

        public async Task<string> AuthorizeWithCode(string instance, string clientId, string clientSecret,
            string code, CancellationToken cancellationToken)
        {
            var accessToken =
                await Api.AuthorizeWithCode(instance, new ClientId(clientId), new ClientSecret(clientSecret), code,
                    cancellationToken);
            return accessToken.Token;
        }

        public async Task<(string, string, Uri)> GetAccountInfo(string instance, string accessToken,
            CancellationToken cancellationToken)
        {
            var account = await Api.GetAccountInformation(instance, new AccessToken(accessToken), cancellationToken);
            return (account.username, account.display_name, new Uri(account.avatar_static));
        }

        public async Task<(string, string)> RegisterClient(string instance, CancellationToken cancellationToken)
        {
            var (id, secret) = await Api.RegisterApp(instance, cancellationToken);
            return (id.Id, secret.Secret);
        }
    }
}
