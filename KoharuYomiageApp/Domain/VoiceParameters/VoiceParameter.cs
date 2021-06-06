using System;

namespace KoharuYomiageApp.Domain.VoiceParameters
{
    public record VoiceParameter
    {
        public VoiceParameter(float globalVolume, uint volume, uint speed, uint tone, uint alpha, uint toneScale,
            uint componentNormal, uint componentHappy, uint componentAnger, uint componentSorrow,
            uint componentCalmness)
        {
            if (globalVolume is < 0f ||
                volume is >100 ||
                speed is >100 ||
                tone is >100 ||
                alpha is >100 ||
                toneScale is >100 ||
                componentNormal is >100 ||
                componentHappy is >100 ||
                componentAnger is >100 ||
                componentSorrow is >100 ||
                componentCalmness is >100)
            {
                throw new AggregateException();
            }

            GlobalVolume = globalVolume;
            Volume = volume;
            Speed = speed;
            Tone = tone;
            Alpha = alpha;
            ToneScale = toneScale;
            ComponentNormal = componentNormal;
            ComponentHappy = componentHappy;
            ComponentAnger = componentAnger;
            ComponentSorrow = componentSorrow;
            ComponentCalmness = componentCalmness;
        }

        public float GlobalVolume { get; }
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
