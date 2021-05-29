using System;
using System.Reactive.Disposables;
using System.Threading;
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
        readonly IWindowLoaded _windowLoaded;
        readonly IStartUpdatingTextList _startUpdatingTextList;
        readonly IStartUpdatingVoiceParameter _startUpdatingVoiceParameter;

        public StartController(IWindowLoaded windowLoaded, IPushStartButton pushStartButton,
            IStartUpdatingTextList startUpdatingTextList, IStartUpdatingVoiceParameter startUpdatingVoiceParameter)
        {
            _windowLoaded = windowLoaded;
            _pushStartButton = pushStartButton;
            _startUpdatingTextList = startUpdatingTextList;
            _startUpdatingVoiceParameter = startUpdatingVoiceParameter;

            _compositeDisposable.Add(_cancellationTokenSource);
        }

        public void Dispose()
        {
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
    }
}
