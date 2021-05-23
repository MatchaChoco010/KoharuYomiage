using KoharuYomiageApp.UseCase.AddMastodonTimelineItem.DataObjects;

namespace KoharuYomiageApp.UseCase.AddMastodonTimelineItem
{
    public interface IMastodonSensitiveStatusReceiver
    {
        void Receive(MastodonSensitiveStatusData data);
    }
}
