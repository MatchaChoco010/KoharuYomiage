using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.TimelineItem.Mastodon;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem
{
    public class MastodonBoostedStatusReceiver : IMastodonBoostedStatusReceiver
    {
        readonly IReadingTextContainerRepository _containerRepository;

        public MastodonBoostedStatusReceiver(IReadingTextContainerRepository containerRepository)
        {
            _containerRepository = containerRepository;
        }

        public void Receive(MastodonBoostedStatusData data)
        {
            var item = new MastodonBoostedStatus(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.BoostedUserDisplayName, data.BoostedUserUserName, data.AuthorDisplayName, data.AuthorUsername,
                data.Content, data.MediaDescriptions);

            var container = _containerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());
        }
    }
}
