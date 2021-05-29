using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.GetGlobalVolume;
using KoharuYomiageApp.UseCase.ReadText;
using KoharuYomiageApp.UseCase.UpdateVoiceParameter;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MainControlController : IDisposable
    {
        readonly CancellationTokenSource _cancellationTokenSource = new();
        readonly IStartReading _startReading;
        readonly IUpdateGlobalVolume _updateGlobalVolume;
        readonly IGetGlobalVolume _getGlobalVolume;

        public MainControlController(IUpdateGlobalVolume updateGlobalVolume, IStartReading startReading, IGetGlobalVolume getGlobalVolume)
        {
            _updateGlobalVolume = updateGlobalVolume;
            _startReading = startReading;
            _getGlobalVolume = getGlobalVolume;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel(true);
            _cancellationTokenSource.Dispose();
        }

        public async Task<double> GetVolume(CancellationToken cancellationToken)
        {
            return await _getGlobalVolume.GetGlobalVolume(cancellationToken);
        }

        public void UpdateVolume(double volume)
        {
            _ = _updateGlobalVolume.Update(volume, _cancellationTokenSource.Token);
        }

        public async Task StartReading(CancellationToken cancellationToken)
        {
            await _startReading.StartReading(cancellationToken);
        }
    }
}
