using System;

namespace KoharuYomiageApp.Entities.VoiceParameters
{
    public record VoiceParameter
    {
        public VoiceParameter(uint volume, uint speed, uint tone, uint alpha, uint toneScale, uint commponentNormal,
            uint componentHappy, uint componentAnger, uint componentSorrow, uint componentCalmness)
        {
            if (volume is >100 ||
                speed is >100 ||
                tone is >100 ||
                alpha is >100 ||
                toneScale is >100 ||
                commponentNormal is >100 ||
                componentHappy is >100 ||
                componentAnger is >100 ||
                componentSorrow is >100 ||
                componentCalmness is >100)
            {
                throw new AggregateException();
            }

            Volume = volume;
            Speed = speed;
            Tone = tone;
            Alpha = alpha;
            ToneScale = toneScale;
            ComponentNormal = commponentNormal;
            ComponentHappy = componentHappy;
            ComponentAnger = componentAnger;
            ComponentSorrow = componentSorrow;
            ComponentCalmness = componentCalmness;
        }

        public uint Volume { get; }
        public uint Speed { get; }
        public uint Tone { get; }
        public uint Alpha { get; }
        public uint ToneScale { get; }
        public uint ComponentNormal { get; }
        public uint ComponentHappy { get; }
        public uint ComponentAnger { get; }
        public uint ComponentSorrow { get; }
        public uint ComponentCalmness { get; }
    }
}
