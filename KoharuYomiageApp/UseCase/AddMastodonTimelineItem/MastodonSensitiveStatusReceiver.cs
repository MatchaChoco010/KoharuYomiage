using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.TimelineItem.Mastodon;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem
{
    public class MastodonSensitiveStatusReceiver : IMastodonSensitiveStatusReceiver
    {
        readonly IReadingTextContainerRepository _readingTextContainerRepository;

        public MastodonSensitiveStatusReceiver(IReadingTextContainerRepository readingTextContainerRepository)
        {
            _readingTextContainerRepository = readingTextContainerRepository;
        }

        public void Receive(MastodonSensitiveStatusData data)
        {
            var status = new MastodonSensitiveStatus(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)), data.AuthorDisplayName,
                data.AuthorUsername, data.SpoilerText, data.Content, data.MediaDescriptions);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(status.ConvertToReadingText());
        }
    }
}
