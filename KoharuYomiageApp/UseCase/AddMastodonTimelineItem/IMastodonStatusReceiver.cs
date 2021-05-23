using KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem
{
    public interface IMastodonStatusReceiver
    {
        void Receive(MastodonStatusData data);
    }
}
