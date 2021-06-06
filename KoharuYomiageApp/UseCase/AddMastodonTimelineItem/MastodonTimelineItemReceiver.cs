using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.TimelineItem.Mastodon;
using KoharuYomiageApp.Presentation.Mastodon.DataObjects;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem
{
    public class MastodonTimelineItemReceiver : IMastodonTimelineItemReceiver
    {
        readonly IReadingTextContainerRepository _readingTextContainerRepository;
        readonly IMastodonAccountRepository _mastodonAccountRepository;

        public MastodonTimelineItemReceiver(IReadingTextContainerRepository readingTextContainerRepository,
            IMastodonAccountRepository mastodonAccountRepository)
        {
            _readingTextContainerRepository = readingTextContainerRepository;
            _mastodonAccountRepository = mastodonAccountRepository;
        }

        public Task ReceiveStatus(MastodonStatusData data, CancellationToken cancellationToken)
        {
            var status = new MastodonStatus(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)), data.AuthorDisplayName,
                data.AuthorUsername, data.Content, data.MediaDescriptions);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(status.ConvertToReadingText());

            return Task.CompletedTask;
        }

        public Task ReceiveSensitiveStatus(MastodonSensitiveStatusData data, CancellationToken cancellationToken)
        {
            var status = new MastodonSensitiveStatus(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)), data.AuthorDisplayName,
                data.AuthorUsername, data.SpoilerText, data.Content, data.MediaDescriptions);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(status.ConvertToReadingText());

            return Task.CompletedTask;
        }

        public Task ReceiveBoostedStatus(MastodonBoostedStatusData data, CancellationToken cancellationToken)
        {
            var item = new MastodonBoostedStatus(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.BoostedUserDisplayName, data.BoostedUserUserName, data.AuthorDisplayName, data.AuthorUsername,
                data.Content, data.MediaDescriptions);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());

            return Task.CompletedTask;
        }

        public Task ReceiveBoostedSensitiveStatus(MastodonBoostedSensitiveStatusData data, CancellationToken cancellationToken)
        {
            var item = new MastodonBoostedSensitiveStatus(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.BoostedUserDisplayName, data.BoostedUserUserName, data.AuthorDisplayName, data.AuthorUsername,
                data.SpoilerText, data.Content, data.MediaDescriptions);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());

            return Task.CompletedTask;
        }

        public Task ReceiveFollowNotification(MastodonFollowNotificationData data, CancellationToken cancellationToken)
        {
            var item = new MastodonFollowNotification(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.AuthorDisplayName, data.AuthorUsername);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());

            return Task.CompletedTask;
        }

        public Task ReceiveFollowRequestNotification(MastodonFollowRequestNotificationData data, CancellationToken cancellationToken)
        {
            var item = new MastodonFollowRequestNotification(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.AuthorDisplayName, data.AuthorUsername);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());

            return Task.CompletedTask;
        }

        public Task ReceiveMentionNotification(MastodonMentionNotificationData data, CancellationToken cancellationToken)
        {
            var item = new MastodonMentionNotification(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.AuthorDisplayName, data.AuthorUsername, data.Content, data.MediaDescriptions);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());

            return Task.CompletedTask;
        }

        public Task ReceiveSensitiveMentionNotification(MastodonSensitiveMentionNotificationData data, CancellationToken cancellationToken)
        {
            var item = new MastodonSensitiveMentionNotification(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.AuthorDisplayName, data.AuthorUsername, data.SpoilerText, data.Content, data.MediaDescriptions);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());

            return Task.CompletedTask;
        }

        public async Task ReceiveReblogNotification(MastodonReblogNotificationData data, CancellationToken cancellationToken)
        {
            var id = new AccountIdentifier(new Username(data.Username), new Instance(data.Instance));

            var account = await _mastodonAccountRepository.FindMastodonAccount(id, cancellationToken);
            if (account is null)
            {
                return;
            }
            var displayName = account.DisplayName.Value;

            var item = new MastodonReblogNotification(id, displayName, data.ReblogUserDisplayName,
                data.ReblogUserUsername, data.Content, data.MediaDescriptions);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());
        }

        public async Task ReceiveSensitiveReblogNotification(MastodonSensitiveReblogNotificationData data, CancellationToken cancellationToken)
        {
            var id = new AccountIdentifier(new Username(data.Username), new Instance(data.Instance));

            var account = await _mastodonAccountRepository.FindMastodonAccount(id, cancellationToken);
            if (account is null)
            {
                return;
            }
            var displayName = account.DisplayName.Value;

            var item = new MastodonSensitiveReblogNotification(id, displayName, data.ReblogUserDisplayName,
                data.ReblogUserUsername, data.SpoilerText, data.Content, data.MediaDescriptions);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());
        }

        public async Task ReceiveFavoriteNotification(MastodonFavoriteNotificationData data, CancellationToken cancellationToken)
        {
            var id = new AccountIdentifier(new Username(data.Username), new Instance(data.Instance));

            var account = await _mastodonAccountRepository.FindMastodonAccount(id, cancellationToken);
            if (account is null)
            {
                return;
            }
            var displayName = account.DisplayName.Value;

            var item = new MastodonFavoriteNotification(id, displayName, data.FavoriteUserDisplayName,
                data.FavoriteUsername, data.Content, data.MediaDescriptions);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());
        }

        public async Task ReceiveSensitiveFavoriteNotification(MastodonSensitiveFavoriteNotificationData data, CancellationToken cancellationToken)
        {
            var id = new AccountIdentifier(new Username(data.Username), new Instance(data.Instance));

            var account = await _mastodonAccountRepository.FindMastodonAccount(id, cancellationToken);
            if (account is null)
            {
                return;
            }
            var displayName = account.DisplayName.Value;

            var item = new MastodonSensitiveFavoriteNotification(id, displayName, data.FavoriteUserDisplayName,
                data.FavoriteUsername, data.SpoilerText, data.Content, data.MediaDescriptions);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());
        }
    }
}
