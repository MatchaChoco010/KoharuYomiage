using System;
using System.Threading;
using KoharuYomiageApp.UseCase.ReadText;
using KoharuYomiageApp.UseCase.UpdateVoiceParameter;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MainControlController : IDisposable
    {
        readonly CancellationTokenSource _cancellationTokenSource = new();
        readonly IStartReading _startReading;
        readonly IStartUpdatingVoiceParameter _startUpdatingVoiceParameter;

        readonly IUpdateGlobalVolume _updateGlobalVolume;

        public MainControlController(IUpdateGlobalVolume updateGlobalVolume,
            IStartUpdatingVoiceParameter startUpdatingVoiceParameter, IStartReading startReading)
        {
            _updateGlobalVolume = updateGlobalVolume;
            _startUpdatingVoiceParameter = startUpdatingVoiceParameter;
            _startReading = startReading;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }

        public void UpdateVolume(double volume)
        {
            _ = _updateGlobalVolume.Update(volume);
        }

        public void StartUpdatingVoiceParameter()
        {
            _ = _startUpdatingVoiceParameter.Start();
        }

        public void StartReading()
        {
            _ = _startReading.StartReading(_cancellationTokenSource.Token);
        }
    }
}
