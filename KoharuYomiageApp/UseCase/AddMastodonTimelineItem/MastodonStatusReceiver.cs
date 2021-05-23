using KoharuYomiageApp.Domain.Account;
using KoharuYomiageApp.Domain.TimelineItem.Mastodon;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects;
using KoharuYomiageApp.UseCase.Repository;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem
{
    public class MastodonStatusReceiver : IMastodonStatusReceiver
    {
        readonly IReadingTextContainerRepository _readingTextContainerRepository;

        public MastodonStatusReceiver(IReadingTextContainerRepository readingTextContainerRepository)
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
