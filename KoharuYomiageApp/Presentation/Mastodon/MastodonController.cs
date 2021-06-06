using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Presentation.Mastodon.DataObjects;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects;

namespace KoharuYomiageApp.Presentation.Mastodon
{
    public class MastodonController
    {
        readonly IMastodonTimelineItemReceiver _itemReceiver;

        public MastodonController(IMastodonTimelineItemReceiver itemReceiver)
        {
            _itemReceiver = itemReceiver;
        }

        public async Task AddMastodonStatus(MastodonStatusInputData inputData, CancellationToken cancellationToken)
        {
            await _itemReceiver.ReceiveStatus(new MastodonStatusData(inputData.Username, inputData.Instance,
                inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.Content, inputData.MediaDescriptions), cancellationToken);
        }

        public async Task AddMastodonSensitiveStatus(MastodonSensitiveStatusInputData inputData, CancellationToken cancellationToken)
        {
            await _itemReceiver.ReceiveSensitiveStatus(new MastodonSensitiveStatusData(inputData.Username, inputData.Instance,
                inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.SpoilerText, inputData.Content,
                inputData.MediaDescriptions), cancellationToken);
        }

        public async Task AddMastodonBoostedStatus(MastodonBoostedStatusInputData inputData, CancellationToken cancellationToken)
        {
            await _itemReceiver.ReceiveBoostedStatus(new MastodonBoostedStatusData(inputData.Username, inputData.Instance,
                inputData.BoostedUserDisplayName, inputData.BoostedUserUserName, inputData.AuthorDisplayName,
                inputData.AuthorUsername, inputData.Content, inputData.MediaDescriptions), cancellationToken);
        }

        public async Task AddMastodonBoostedSensitiveStatus(MastodonBoostedSensitiveStatusInputData inputData, CancellationToken cancellationToken)
        {
            await _itemReceiver.ReceiveBoostedSensitiveStatus(new MastodonBoostedSensitiveStatusData(inputData.Username,
                inputData.Instance, inputData.BoostedUserDisplayName, inputData.BoostedUserUserName,
                inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.SpoilerText, inputData.Content,
                inputData.MediaDescriptions), cancellationToken);
        }

        public async Task AddMastodonFollowNotification(MastodonFollowNotificationData inputData, CancellationToken cancellationToken)
        {
            await _itemReceiver.ReceiveFollowNotification(new MastodonFollowNotificationData(inputData.Username,
                inputData.Instance, inputData.AuthorDisplayName, inputData.AuthorUsername), cancellationToken);
        }

        public async Task AddMastodonFollowRequestNotification(MastodonFollowRequestNotificationData inputData, CancellationToken cancellationToken)
        {
            await _itemReceiver.ReceiveFollowRequestNotification(new MastodonFollowRequestNotificationData(
                inputData.Username, inputData.Instance, inputData.AuthorDisplayName, inputData.AuthorUsername), cancellationToken);
        }

        public async Task AddMastodonMentionNotification(MastodonMentionNotificationData inputData, CancellationToken cancellationToken)
        {
            await _itemReceiver.ReceiveMentionNotification(new MastodonMentionNotificationData(inputData.Username,
                inputData.Instance, inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.Content,
                inputData.MediaDescriptions), cancellationToken);
        }

        public async Task AddMastodonSensitiveMentionNotification(MastodonSensitiveMentionNotificationData inputData, CancellationToken cancellationToken)
        {
            await _itemReceiver.ReceiveSensitiveMentionNotification(new MastodonSensitiveMentionNotificationData(
                inputData.Username, inputData.Instance, inputData.AuthorDisplayName, inputData.AuthorUsername,
                inputData.SpoilerText, inputData.Content, inputData.MediaDescriptions), cancellationToken);
        }

        public async Task AddMastodonReblogNotification(MastodonReblogNotificationData inputData, CancellationToken cancellationToken)
        {
            await _itemReceiver.ReceiveReblogNotification(new MastodonReblogNotificationData(inputData.Username,
                inputData.Instance, inputData.ReblogUserDisplayName, inputData.ReblogUserUsername, inputData.Content,
                inputData.MediaDescriptions), cancellationToken);
        }

        public async Task AddMastodonSensitiveReblogNotification(MastodonSensitiveReblogNotificationData inputData, CancellationToken cancellationToken)
        {
            await _itemReceiver.ReceiveSensitiveReblogNotification(new MastodonSensitiveReblogNotificationData(
                inputData.Username, inputData.Instance, inputData.ReblogUserDisplayName, inputData.ReblogUserUsername,
                inputData.SpoilerText, inputData.Content, inputData.MediaDescriptions), cancellationToken);
        }

        public async Task AddMastodonFavoriteNotification(MastodonFavoriteNotificationData inputData, CancellationToken cancellationToken)
        {
            await _itemReceiver.ReceiveFavoriteNotification(new MastodonFavoriteNotificationData(inputData.Username,
                inputData.Instance, inputData.FavoriteUserDisplayName, inputData.FavoriteUsername, inputData.Content,
                inputData.MediaDescriptions), cancellationToken);
        }

        public async Task AddMastodonSensitiveFavoriteNotification(MastodonSensitiveFavoriteNotificationData inputData, CancellationToken cancellationToken)
        {
            await _itemReceiver.ReceiveSensitiveFavoriteNotification(new MastodonSensitiveFavoriteNotificationData(
                inputData.Username, inputData.Instance, inputData.FavoriteUserDisplayName, inputData.FavoriteUsername,
                inputData.SpoilerText, inputData.Content, inputData.MediaDescriptions), cancellationToken);
        }
    }
}
