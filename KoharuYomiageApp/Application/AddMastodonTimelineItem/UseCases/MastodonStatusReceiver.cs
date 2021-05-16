using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.TimelineItem.Mastodon;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases
{
    public class MastodonStatusReceiver : IMastodonStatusReceiver
    {
        readonly ReadingTextContainerRepository _readingTextContainerRepository;

        public MastodonStatusReceiver(ReadingTextContainerRepository readingTextContainerRepository)
        {
            _readingTextContainerRepository = readingTextContainerRepository;
        }

        public void Receive(MastodonStatusData data)
        {
            var status = new MastodonStatus(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)), data.AuthorDisplayName,
                data.AuthorUsername, data.Content, data.MediaDescriptions);

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(status.ConvertToReadingText());
        }
    }
}
