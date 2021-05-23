using System;
using System.Reactive.Subjects;
using KoharuYomiageApp.Domain.Account;

namespace KoharuYomiageApp.Domain.VoiceParameters
{
    public abstract class VoiceProfile
    {
        readonly Subject<VoiceProfile> _onUpdate = new();

        double _alpha = 0.5;

        double _componenCalmness;

        double _componentAnger;

        double _componentHappy;

        double _componentNormal = 1;

        double _componentSorrow;

        double _speed = 0.5;

        double _tone = 0.5;

        double _toneScale = 0.5;

        double _volume = 0.5;

        VoiceProfile(AccountIdentifier accountIdentifier)
        {
            AccountIdentifier = accountIdentifier;
        }

        public AccountIdentifier AccountIdentifier { get; }

        public double Volume
        {
            get => _volume;
            private set
            {
                if (value is <0.0 or >1.0)
                {
                    throw new ArgumentException();
                }

                _volume = value;
                _onUpdate.OnNext(this);
            }
        }

        public double Speed
        {
            get => _speed;
            private set
            {
                if (value is <0.0 or >1.0)
                {
                    throw new ArgumentException();
                }

                _speed = value;
                _onUpdate.OnNext(this);
            }
        }

        public double Tone
        {
            get => _tone;
            private set
            {
                if (value is <0.0 or >1.0)
                {
                    throw new ArgumentException();
                }

                _tone = value;
                _onUpdate.OnNext(this);
            }
        }

        public double Alpha
        {
            get => _alpha;
            private set
            {
                if (value is <0.0 or >1.0)
                {
                    throw new ArgumentException();
                }

                _alpha = value;
                _onUpdate.OnNext(this);
            }
        }

        public double ToneScale
        {
            get => _toneScale;
            private set
            {
                if (value is <0.0 or >1.0)
                {
                    throw new ArgumentException();
                }

                _toneScale = value;
                _onUpdate.OnNext(this);
            }
        }

        public double ComponentNormal
        {
            get => _componentNormal;
            private set
            {
                if (value is <0.0 or >1.0)
                {
                    throw new ArgumentException();
                }

                _componentNormal = value;
                _onUpdate.OnNext(this);
            }
        }

        public double ComponentHappy
        {
            get => _componentHappy;
            private set
            {
                if (value is <0.0 or >1.0)
                {
                    throw new ArgumentException();
                }

                _componentHappy = value;
                _onUpdate.OnNext(this);
            }
        }

        public double ComponentAnger
        {
            get => _componentAnger;
            private set
            {
                if (value is <0.0 or >1.0)
                {
                    throw new ArgumentException();
                }

                _componentAnger = value;
                _onUpdate.OnNext(this);
            }
        }

        public double ComponentSorrow
        {
            get => _componentSorrow;
            private set
            {
                if (value is <0.0 or >1.0)
                {
                    throw new ArgumentException();
                }

                _componentSorrow = value;
                _onUpdate.OnNext(this);
            }
        }

        public double ComponenCalmness
        {
            get => _componenCalmness;
            private set
            {
                if (value is <0.0 or >1.0)
                {
                    throw new ArgumentException();
                }

                _componenCalmness = value;
                _onUpdate.OnNext(this);
            }
        }

        public IObservable<VoiceProfile> OnUpdate => _onUpdate;

        public void Update(double volume, double speed, double tone, double alpha, double toneScale,
            double componentNormal,
            double componentHappy, double componentAnger, double componentSorrow, double componentCalmness)
        {
            Volume = volume;
            Speed = speed;
            Tone = tone;
            Alpha = alpha;
            ToneScale = toneScale;
            ComponentNormal = componentNormal;
            ComponentHappy = componentHappy;
            ComponentAnger = componentAnger;
            ComponentSorrow = componentSorrow;
            ComponenCalmness = componentCalmness;
        }

        public class MastodonStatusVoiceProfile : VoiceProfile
        {
            public MastodonStatusVoiceProfile(AccountIdentifier accountIdentifier) : base(accountIdentifier)
            {
            }
        }

        public class MastodonSensitiveStatusVoiceProfile : VoiceProfile
        {
            public MastodonSensitiveStatusVoiceProfile(AccountIdentifier accountIdentifier) : base(accountIdentifier)
            {
            }
        }

        public class MastodonBoostedStatusVoiceProfile : VoiceProfile
        {
            public MastodonBoostedStatusVoiceProfile(AccountIdentifier accountIdentifier) : base(accountIdentifier)
            {
            }
        }

        public class MastodonBoostedSensitiveStatusVoiceProfile : VoiceProfile
        {
            public MastodonBoostedSensitiveStatusVoiceProfile(AccountIdentifier accountIdentifier) : base(
                accountIdentifier)
            {
            }
        }
    }
}
