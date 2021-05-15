using KoharuYomiageApp.Application.WindowLoaded.UseCases;

namespace KoharuYomiageApp.Application.WindowLoaded.Interfaces
{
    public class PushStartButtonController
    {
        readonly IPushStartButton _pushStartButton;

        public PushStartButtonController(IPushStartButton pushStartButton)
        {
            _pushStartButton = pushStartButton;
        }

        public void PushStartButton()
        {
            _ = _pushStartButton.PushStartButton();
        }
    }
}
