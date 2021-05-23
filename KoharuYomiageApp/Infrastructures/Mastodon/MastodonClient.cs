using System;
using System.Linq;
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
                .Subscribe(item =>
                {
                    switch (item)
                    {
                        case UserStreamPayload.Status(var status):
                            switch (status)
                            {
                                case {sensitive: true, reblog: null}:
                                case {spoiler_text: var spoilerText, reblog: null}
                                    when !string.IsNullOrEmpty(spoilerText):
                                    _mastodonController.AddMastodonSensitiveStatus(
                                        new MastodonSensitiveStatusInputData(username, instance,
                                            status.account.display_name, status.account.username,
                                            status.spoiler_text, status.content,
                                            status.media_attachments.Select(media => media.description ?? "")));
                                    break;
                                case {sensitive: false, reblog: null}:
                                    _mastodonController.AddMastodonStatus(new MastodonStatusInputData(
                                        username, instance, status.account.display_name, status.account.username,
                                        status.content,
                                        status.media_attachments.Select(media => media.description ?? "")));
                                    break;
                                case {sensitive: true, reblog: not null}:
                                case {spoiler_text: var spoilerText, reblog: not null}
                                    when !string.IsNullOrEmpty(spoilerText):
                                    _mastodonController.AddMastodonBoostedSensitiveStatus(
                                        new MastodonBoostedSensitiveStatusInputData(username, instance,
                                            status.account.display_name, status.account.username,
                                            status.reblog.account.display_name, status.reblog.account.username,
                                            status.spoiler_text, status.content,
                                            status.media_attachments.Select(media => media.description ?? "")));
                                    break;
                                case {sensitive: false, reblog: not null}:
                                    _mastodonController.AddMastodonBoostedStatus(
                                        new MastodonBoostedStatusInputData(username, instance,
                                            status.account.display_name, status.account.username,
                                            status.reblog.account.display_name, status.reblog.account.username,
                                            status.content,
                                            status.media_attachments.Select(media => media.description ?? "")));
                                    break;
                            }

                            break;
                        case UserStreamPayload.Notification(var notification):
                            // TODO
                            break;
                    }
                });
        }

        public async Task<string> AuthorizeWithCode(string instance, string clientId, string clientSecret,
            string code)
        {
            var accessToken =
                await Api.AuthorizeWithCode(instance, new ClientId(clientId), new ClientSecret(clientSecret), code);
            return accessToken.Token;
        }

        public async Task<(string, Uri)> GetAccountInfo(string instance, string accessToken)
        {
            var account = await Api.GetAccountInformation(instance, new AccessToken(accessToken));
            return (account.username, new Uri(account.avatar_static));
        }

        public async Task<(string, string)> RegisterClient(string instance)
        {
            var (id, secret) = await Api.RegisterApp(instance);
            return (id.Id, secret.Secret);
        }
    }
}
