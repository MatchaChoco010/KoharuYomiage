using System;
using Reactive.Bindings;

namespace KoharuYomiageApp.Domain.VoiceParameters
{
    public class GlobalVolume : IDisposable
    {
        public GlobalVolume(double volume = 0.65)
        {
            Volume = new ReactivePropertySlim<double>(volume);
        }

        public ReactivePropertySlim<double> Volume { get; }

        public double GetMultiplier()
        {
            return Volume.Value / 0.65;
        }

        public void Dispose()
        {
            Volume.Dispose();
        }
    }
}
