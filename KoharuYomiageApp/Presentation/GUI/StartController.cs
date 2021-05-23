using KoharuYomiageApp.UseCase.WindowLoaded;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class StartController
    {
        readonly IPushStartButton _pushStartButton;
        readonly IWindowLoaded _windowLoaded;


        public StartController(IWindowLoaded windowLoaded, IPushStartButton pushStartButton)
        {
            _windowLoaded = windowLoaded;
            _pushStartButton = pushStartButton;
        }

        public void WindowLoaded()
        {
            _ = _windowLoaded.WindowLoaded();
        }

        public void PushStartButton()
        {
            _ = _pushStartButton.PushStartButton();
        }
    }
}
