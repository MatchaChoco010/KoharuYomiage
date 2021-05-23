using KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem
{
    public interface IMastodonBoostedStatusReceiver
    {
        void Receive(MastodonBoostedStatusData data);
    }
}
