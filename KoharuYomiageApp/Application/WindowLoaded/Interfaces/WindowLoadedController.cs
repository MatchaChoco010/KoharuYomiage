using KoharuYomiageApp.Application.WindowLoaded.UseCases;

namespace KoharuYomiageApp.Application.WindowLoaded.Interfaces
{
    public class WindowLoadedController
    {
        readonly IWindowLoaded _windowLoaded;

        public WindowLoadedController(IWindowLoaded windowLoaded)
        {
            _windowLoaded = windowLoaded;
        }

        public void WindowLoaded()
        {
            _ = _windowLoaded.LoadedWindow();
        }
    }
}
