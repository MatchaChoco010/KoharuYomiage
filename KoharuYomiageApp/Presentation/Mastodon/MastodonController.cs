using KoharuYomiageApp.Presentation.Mastodon.DataObjects;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects;

namespace KoharuYomiageApp.Presentation.Mastodon
{
    public class MastodonController
    {
        readonly IMastodonBoostedSensitiveStatusReceiver _boostedSensitiveStatusReceiver;
        readonly IMastodonBoostedStatusReceiver _boostedStatusReceiver;
        readonly IMastodonSensitiveStatusReceiver _sensitiveStatusReceiver;
        readonly IMastodonStatusReceiver _statusReceiver;

        public MastodonController(IMastodonStatusReceiver statusReceiver,
            IMastodonSensitiveStatusReceiver sensitiveStatusReceiver,
            IMastodonBoostedStatusReceiver boostedStatusReceiver,
            IMastodonBoostedSensitiveStatusReceiver boostedSensitiveStatusReceiver)
        {
            _statusReceiver = statusReceiver;
            _sensitiveStatusReceiver = sensitiveStatusReceiver;
            _boostedStatusReceiver = boostedStatusReceiver;
            _boostedSensitiveStatusReceiver = boostedSensitiveStatusReceiver;
        }

        public void AddMastodonStatus(MastodonStatusInputData inputData)
        {
            _statusReceiver.Receive(new MastodonStatusData(inputData.Username, inputData.Instance,
                inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.Content, inputData.MediaDescriptions));
        }

        public void AddMastodonSensitiveStatus(MastodonSensitiveStatusInputData inputData)
        {
            _sensitiveStatusReceiver.Receive(new MastodonSensitiveStatusData(inputData.Username, inputData.Instance,
                inputData.AuthorDisplayName, inputData.AuthorUsername, inputData.SpoilerText, inputData.Content,
                inputData.MediaDescriptions));
        }

        public void AddMastodonBoostedStatus(MastodonBoostedStatusInputData inputData)
        {
            _boostedStatusReceiver.Receive(new MastodonBoostedStatusData(inputData.Username, inputData.Instance,
                inputData.BoostedUserDisplayName, inputData.BoostedUserUserName, inputData.AuthorDisplayName,
                inputData.AuthorUsername, inputData.Content, inputData.MediaDescriptions));
        }

        public void AddMastodonBoostedSensitiveStatus(MastodonBoostedSensitiveStatusInputData inputData)
        {
            _boostedSensitiveStatusReceiver.Receive(new MastodonBoostedSensitiveStatusData(inputData.Username,
                inputData.Instance,
                inputData.BoostedUserDisplayName, inputData.BoostedUserUserName, inputData.AuthorDisplayName,
                inputData.AuthorUsername, inputData.SpoilerText, inputData.Content, inputData.MediaDescriptions));
        }
    }
}
