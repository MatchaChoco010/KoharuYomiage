using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.TimelineItem.Mastodon;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases
{
    public class MastodonBoostedSensitiveStatusReceiver : IMastodonBoostedSensitiveStatusReceiver
    {
        readonly ReadingTextContainerRepository _containerRepository;

        public MastodonBoostedSensitiveStatusReceiver(ReadingTextContainerRepository containerRepository)
        {
            _containerRepository = containerRepository;
        }

        public void Receive(MastodonBoostedSensitiveStatusData data)
        {
            var item = new MastodonBoostedSensitiveStatus(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)),
                data.BoostedUserDisplayName, data.BoostedUserUserName, data.AuthorDisplayName, data.AuthorUsername,
                data.SpoilerText, data.Content, data.MediaDescriptions);

            var container = _containerRepository.GetContainer();

            container.Add(item.ConvertToReadingText());
        }
    }
}
