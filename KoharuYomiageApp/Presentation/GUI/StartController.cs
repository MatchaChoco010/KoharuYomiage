using System;
using System.Threading;
using KoharuYomiageApp.UseCase.WindowLoaded;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class StartController : IDisposable
    {
        readonly IPushStartButton _pushStartButton;
        readonly IWindowLoaded _windowLoaded;
        readonly CancellationTokenSource _cancellationTokenSource = new();

        public StartController(IWindowLoaded windowLoaded, IPushStartButton pushStartButton)
        {
            _windowLoaded = windowLoaded;
            _pushStartButton = pushStartButton;
        }

        public void WindowLoaded()
        {
            _ = _windowLoaded.WindowLoaded(_cancellationTokenSource.Token);
        }

        public void PushStartButton()
        {
            _ = _pushStartButton.PushStartButton(_cancellationTokenSource.Token);
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }
    }
}
