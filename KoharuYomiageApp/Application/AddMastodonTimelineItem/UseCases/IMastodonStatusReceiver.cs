using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases
{
    public interface IMastodonStatusReceiver
    {
        void Receive(MastodonStatusData data);
    }
}
