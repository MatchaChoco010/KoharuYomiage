using KoharuYomiageApp.Application.UpdateVoiceParameters.UseCases;

namespace KoharuYomiageApp.Application.UpdateVoiceParameters.Interfaces
{
    public class UpdateGlobalVolumeController
    {
        readonly IUpdateGlobalVolume _updateGlobalVolume;

        public UpdateGlobalVolumeController(IUpdateGlobalVolume updateGlobalVolume)
        {
            _updateGlobalVolume = updateGlobalVolume;
        }

        public void Update(double volume)
        {
            _ = _updateGlobalVolume.Update(volume);
        }
    }
}
