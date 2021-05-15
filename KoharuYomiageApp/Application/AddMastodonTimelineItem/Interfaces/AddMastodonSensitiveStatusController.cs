using KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces.DataObjects;
using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases;
using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces
{
    public class AddMastodonSensitiveStatusController
    {
        readonly IMastodonSensitiveStatusReceiver _receiver;

        public AddMastodonSensitiveStatusController(IMastodonSensitiveStatusReceiver receiver)
        {
            _receiver = receiver;
        }

        public void AddMastodonSensitiveStatus(MastodonSensitiveStatusInputData inputData)
        {
            _receiver.Receive(new MastodonSensitiveStatusData(inputData.Username, inputData.Instance,
                inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.SpoilerText, inputData.Content,
                inputData.Muted, inputData.MediaDescriptions));
        }
    }
}
