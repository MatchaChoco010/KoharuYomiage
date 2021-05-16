using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.AddMastodonAccount.Interfaces;
using KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces;
using KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces.DataObjects;
using MastodonApi;
using MastodonApi.Payloads;

namespace KoharuYomiageApp.Infrastructures
{
    public class MastodonApiService : Application.WindowLoaded.Interfaces.IMastodonApiAddAccountToReaderService,
        IMastodonApiRegisterClientService,
        IMastodonApiAuthorizeAccountWithCodeService,
        IMastodonApiGetAccountInfoService,
        IMastodonApiAddAccountToReaderService
    {
        readonly AddMastodonBoostedSensitiveStatusController _addMastodonBoostedSensitiveStatusController;
        readonly AddMastodonBoostedStatusController _addMastodonBoostedStatusController;
        readonly AddMastodonSensitiveStatusController _addMastodonSensitiveStatusController;
        readonly AddMastodonStatusController _addMastodonStatusController;
        readonly Dictionary<string, IDisposable> _connections = new();

        public MastodonApiService(AddMastodonStatusController addMastodonStatusController,
            AddMastodonSensitiveStatusController addMastodonSensitiveStatusController,
            AddMastodonBoostedStatusController addMastodonBoostedStatusController,
            AddMastodonBoostedSensitiveStatusController addMastodonBoostedSensitiveStatusController)
        {
            _addMastodonStatusController = addMastodonStatusController;
            _addMastodonSensitiveStatusController = addMastodonSensitiveStatusController;
            _addMastodonBoostedStatusController = addMastodonBoostedStatusController;
            _addMastodonBoostedSensitiveStatusController = addMastodonBoostedSensitiveStatusController;
        }

        public void AddAccountToReader(string accountIdentifier, string username, string instance, string accessToken)
        {
            if (_connections.ContainsKey(accountIdentifier))
            {
                _connections[accountIdentifier].Dispose();
                _connections.Remove(accountIdentifier);
            }

            var disposable =
                Api.GetUserStreamingObservable(instance, new AccessToken(accessToken))
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
                                        _addMastodonSensitiveStatusController.AddMastodonSensitiveStatus(
                                            new MastodonSensitiveStatusInputData(username, instance,
                                                status.account.display_name, status.account.username,
                                                status.spoiler_text ?? "", status.content,
                                                status.media_attachments.Select(media => media.description ?? "")));
                                        break;
                                    case {sensitive: false, reblog: null}:
                                        _addMastodonStatusController.AddMastodonStatus(new MastodonStatusInputData(
                                            username, instance, status.account.display_name, status.account.username,
                                            status.content,
                                            status.media_attachments.Select(media => media.description ?? "")));
                                        break;
                                    case {sensitive: true, reblog: not null}:
                                    case {spoiler_text: var spoilerText, reblog: not null}
                                        when !string.IsNullOrEmpty(spoilerText):
                                        _addMastodonBoostedSensitiveStatusController.AddMastodonBoostedSensitiveStatus(
                                            new MastodonBoostedSensitiveStatusInputData(username, instance,
                                                status.account.display_name, status.account.username,
                                                status.reblog.account.display_name, status.reblog.account.username,
                                                status.spoiler_text ?? "", status.content,
                                                status.media_attachments.Select(media => media.description ?? "")));
                                        break;
                                    case {sensitive: false, reblog: not null}:
                                        _addMastodonBoostedStatusController.AddMastodonBoostedStatus(
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
            _connections.Add(accountIdentifier, disposable);
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
