using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.ReadText;
using KoharuYomiageApp.UseCase.UpdateTextList;
using KoharuYomiageApp.UseCase.UpdateVoiceParameter;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MainControlController : IDisposable
    {
        readonly CancellationTokenSource _cancellationTokenSource = new();
        readonly IStartReading _startReading;
        readonly IUpdateGlobalVolume _updateGlobalVolume;

        public MainControlController(IUpdateGlobalVolume updateGlobalVolume, IStartReading startReading)
        {
            _updateGlobalVolume = updateGlobalVolume;
            _startReading = startReading;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }

        public void UpdateVolume(double volume)
        {
            _ = _updateGlobalVolume.Update(volume, _cancellationTokenSource.Token);
        }

        public Task StartReading(CancellationToken cancellationToken)
        {
            return _startReading.StartReading(cancellationToken);
        }
    }
}
