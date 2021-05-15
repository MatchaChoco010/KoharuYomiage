using KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces.DataObjects;
using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases;
using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces
{
    public class AddMastodonBoostedSensitiveStatusController
    {
        readonly IMastodonBoostedSensitiveStatusReceiver _receiver;

        public AddMastodonBoostedSensitiveStatusController(IMastodonBoostedSensitiveStatusReceiver receiver)
        {
            _receiver = receiver;
        }

        public void AddMastodonBoostedSensitiveStatus(MastodonBoostedSensitiveStatusInputData inputData)
        {
            _receiver.Receive(new MastodonBoostedSensitiveStatusData(inputData.Username, inputData.Instance,
                inputData.BoostedUserDisplayName, inputData.BoostedUserUserName, inputData.AuthorDisplayName,
                inputData.AuthorUsername, inputData.SpoilerText, inputData.Content, inputData.Muted,
                inputData.MediaDescriptions));
        }
    }
}
