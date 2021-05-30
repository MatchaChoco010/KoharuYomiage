using System;
using System.Reactive.Disposables;
using System.Threading;
using KoharuYomiageApp.UseCase.ReadingTextContainerSize;
using KoharuYomiageApp.UseCase.UpdateTextList;
using KoharuYomiageApp.UseCase.UpdateVoiceParameter;
using KoharuYomiageApp.UseCase.WindowLoaded;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class StartController : IDisposable
    {
        readonly CancellationTokenSource _cancellationTokenSource = new();
        readonly CompositeDisposable _compositeDisposable = new();
        readonly IPushStartButton _pushStartButton;
        readonly IStartUpdatingTextList _startUpdatingTextList;
        readonly IStartUpdatingVoiceParameter _startUpdatingVoiceParameter;
        readonly IWindowLoaded _windowLoaded;
        readonly IInitializeReadingTextContainerSize _initializeReadingTextContainerSize;

        public StartController(IWindowLoaded windowLoaded, IPushStartButton pushStartButton,
            IStartUpdatingTextList startUpdatingTextList, IStartUpdatingVoiceParameter startUpdatingVoiceParameter,
            IInitializeReadingTextContainerSize initializeReadingTextContainerSize)
        {
            _windowLoaded = windowLoaded;
            _pushStartButton = pushStartButton;
            _startUpdatingTextList = startUpdatingTextList;
            _startUpdatingVoiceParameter = startUpdatingVoiceParameter;
            _initializeReadingTextContainerSize = initializeReadingTextContainerSize;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel(true);
            _cancellationTokenSource.Dispose();
            _compositeDisposable.Dispose();
        }

        public void WindowLoaded()
        {
            _ = _windowLoaded.WindowLoaded(_cancellationTokenSource.Token);
        }

        public void PushStartButton()
        {
            _ = _pushStartButton.PushStartButton(_cancellationTokenSource.Token);
        }

        public void StartUpdatingVoiceParameter()
        {
            _ = _startUpdatingVoiceParameter.Start(_cancellationTokenSource.Token);
        }

        public void StartUpdatingTextList()
        {
            _startUpdatingTextList.StartUpdatingTextList().AddTo(_compositeDisposable);
        }

        public void InitializeReadingTextContainerSize()
        {
            _ = _initializeReadingTextContainerSize.InitializeReadingTextContainerSize(_cancellationTokenSource.Token);
        }
    }
}
