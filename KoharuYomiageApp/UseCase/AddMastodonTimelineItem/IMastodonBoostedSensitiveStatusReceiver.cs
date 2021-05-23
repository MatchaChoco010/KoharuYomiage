using KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem
{
    public interface IMastodonBoostedSensitiveStatusReceiver
    {
        void Receive(MastodonBoostedSensitiveStatusData data);
    }
}
