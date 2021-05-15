using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.TimelineItem.Mastodon;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases
{
    public class MastodonBoostedStatusReceiver : IMastodonBoostedStatusReceiver
    {
        readonly ReadingTextContainerRepository _containerRepository;

        public MastodonBoostedStatusReceiver(ReadingTextContainerRepository containerRepository)
        {
            _containerRepository = containerRepository;
        }

        public void Receive(MastodonBoostedStatusData data)
        {
            var item = new MastodonBoostedStatus(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.BoostedUserDisplayName, data.BoostedUserUserName, data.AuthorDisplayName, data.AuthorUsername,
                data.Content, data.Muted, data.MediaDescriptions);

            if (item.Muted)
            {
                return;
            }

            var container = _containerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());
        }
    }
}
