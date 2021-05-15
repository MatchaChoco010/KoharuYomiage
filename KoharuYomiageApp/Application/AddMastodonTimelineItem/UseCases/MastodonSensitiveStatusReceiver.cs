using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Entities.Account;
using KoharuYomiageApp.Entities.TimelineItem.Mastodon;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases
{
    public class MastodonSensitiveStatusReceiver : IMastodonSensitiveStatusReceiver
    {
        readonly ReadingTextContainerRepository _readingTextContainerRepository;

        public MastodonSensitiveStatusReceiver(ReadingTextContainerRepository readingTextContainerRepository)
        {
            _readingTextContainerRepository = readingTextContainerRepository;
        }

        public void Receive(MastodonSensitiveStatusData data)
        {
            var status = new MastodonSensitiveStatus(
                new AccountIdentifier(new Username(data.Username), new Instance(data.Instance)), data.AuthorDisplayName,
                data.AuthorUsername, data.SpoilerText, data.Content, data.Muted, data.MediaDescriptions);

            if (status.Muted)
            {
                return;
            }

            var container = _readingTextContainerRepository.GetContainer();

            container.Add(status.ConvertToReadingText());
        }
    }
}
