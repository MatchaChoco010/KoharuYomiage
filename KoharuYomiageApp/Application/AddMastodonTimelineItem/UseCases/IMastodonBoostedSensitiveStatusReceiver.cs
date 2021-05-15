using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases
{
    public interface IMastodonBoostedSensitiveStatusReceiver
    {
        void Receive(MastodonBoostedSensitiveStatusData data);
    }
}
