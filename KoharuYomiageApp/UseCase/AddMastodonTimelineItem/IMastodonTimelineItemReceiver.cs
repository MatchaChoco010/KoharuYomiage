using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Presentation.Mastodon.DataObjects;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem
{
    public interface IMastodonTimelineItemReceiver
    {
        Task ReceiveStatus(MastodonStatusData data, CancellationToken cancellationToken);
        Task ReceiveSensitiveStatus(MastodonSensitiveStatusData data, CancellationToken cancellationToken);
        Task ReceiveBoostedStatus(MastodonBoostedStatusData data, CancellationToken cancellationToken);
        Task ReceiveBoostedSensitiveStatus(MastodonBoostedSensitiveStatusData data, CancellationToken cancellationToken);
        Task ReceiveFollowNotification(MastodonFollowNotificationData data, CancellationToken cancellationToken);
        Task ReceiveFollowRequestNotification(MastodonFollowRequestNotificationData data, CancellationToken cancellationToken);
        Task ReceiveMentionNotification(MastodonMentionNotificationData data, CancellationToken cancellationToken);
        Task ReceiveSensitiveMentionNotification(MastodonSensitiveMentionNotificationData data, CancellationToken cancellationToken);
        Task ReceiveReblogNotification(MastodonReblogNotificationData data, CancellationToken cancellationToken);
        Task ReceiveSensitiveReblogNotification(MastodonSensitiveReblogNotificationData data, CancellationToken cancellationToken);
        Task ReceiveFavoriteNotification(MastodonFavoriteNotificationData data, CancellationToken cancellationToken);
        Task ReceiveSensitiveFavoriteNotification(MastodonSensitiveFavoriteNotificationData data, CancellationToken cancellationToken);
    }
}
