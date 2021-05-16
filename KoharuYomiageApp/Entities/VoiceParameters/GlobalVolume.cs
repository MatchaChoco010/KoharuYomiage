using System;
using System.Reactive.Subjects;

namespace KoharuYomiageApp.Entities.VoiceParameters
{
    public class GlobalVolume
    {
        readonly Subject<GlobalVolume> _onUpdate = new();

        public GlobalVolume(double volume = 0.65)
        {
            Volume = volume;
        }

        public IObservable<GlobalVolume> OnUpdate => _onUpdate;

        public double Volume { get; private set; }

        public void Update(double newVolume)
        {
            Volume = newVolume;
            _onUpdate.OnNext(this);
        }

        public double GetMultiplier()
        {
            return Volume / 0.65;
        }
    }
}
