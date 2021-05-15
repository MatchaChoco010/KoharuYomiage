using KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.AddMastodonTimelineItem.UseCases
{
    public interface IMastodonSensitiveStatusReceiver
    {
        void Receive(MastodonSensitiveStatusData data);
    }
}
