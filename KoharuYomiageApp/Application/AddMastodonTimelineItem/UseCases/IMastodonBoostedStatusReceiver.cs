using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases
{
    public interface IMastodonBoostedStatusReceiver
    {
        void Receive(MastodonBoostedStatusData data);
    }
}
