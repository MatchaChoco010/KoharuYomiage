using KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces.DataObjects;
using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases;
using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces
{
    public class AddMastodonStatusController
    {
        readonly IMastodonStatusReceiver _receiver;

        public AddMastodonStatusController(IMastodonStatusReceiver receiver)
        {
            _receiver = receiver;
        }

        public void AddMastodonStatus(MastodonStatusInputData inputData)
        {
            _receiver.Receive(new MastodonStatusData(inputData.Username, inputData.Instance,
                inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.Content, inputData.MediaDescriptions));
        }
    }
}
