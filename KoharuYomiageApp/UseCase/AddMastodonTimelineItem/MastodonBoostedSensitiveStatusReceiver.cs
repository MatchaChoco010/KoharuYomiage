using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.TimelineItem.Mastodon;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem
{
    public class MastodonBoostedSensitiveStatusReceiver : IMastodonBoostedSensitiveStatusReceiver
    {
        readonly IReadingTextContainerRepository _containerRepository;

        public MastodonBoostedSensitiveStatusReceiver(IReadingTextContainerRepository containerRepository)
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
