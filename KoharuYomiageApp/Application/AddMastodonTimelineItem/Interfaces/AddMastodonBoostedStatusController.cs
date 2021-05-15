using KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces.DataObjects;
using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases;
using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.Interfaces
{
    public class AddMastodonBoostedStatusController
    {
        readonly IMastodonBoostedStatusReceiver _receiver;

        public AddMastodonBoostedStatusController(IMastodonBoostedStatusReceiver receiver)
        {
            _receiver = receiver;
        }

        public void AddMastodonBoostedStatus(MastodonBoostedStatusInputData inputData)
        {
            _receiver.Receive(new MastodonBoostedStatusData(inputData.Username, inputData.Instance,
                inputData.BoostedUserDisplayName, inputData.BoostedUserUserName, inputData.AuthorDisplayName,
                inputData.AuthorUsername, inputData.Content, inputData.Muted, inputData.MediaDescriptions));
        }
    }
}
