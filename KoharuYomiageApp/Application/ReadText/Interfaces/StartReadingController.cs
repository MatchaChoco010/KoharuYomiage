using System;
using System.Threading;
using KoharuYomiageApp.Application.ReadText.UseCases;

namespace KoharuYomiageApp.Application.ReadText.Interfaces
{
    public class StartReadingController : IDisposable
    {
        readonly CancellationTokenSource _cancellationTokenSource = new();
        readonly IStartReading _startReading;

        public StartReadingController(IStartReading startReading)
        {
            _startReading = startReading;
        }

        public void Dispose()
        {
            _cancellationTokenSource.Dispose();
        }

        public void StartReading()
        {
            _ = _startReading.StartReading(_cancellationTokenSource.Token);
        }
    }
}
