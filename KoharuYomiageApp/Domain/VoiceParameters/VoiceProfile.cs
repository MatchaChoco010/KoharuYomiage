using System;
using System.Reactive.Subjects;
using KoharuYomiageApp.Domain.Account;

namespace KoharuYomiageApp.Domain.VoiceParameters
{
    public abstract class VoiceProfile : IDisposable
    {
        readonly Subject<VoiceProfile> _onUpdate = new();

        VoiceProfile(AccountIdentifier accountIdentifier)
        {
            AccountIdentifier = accountIdentifier;
        }

        public AccountIdentifier AccountIdentifier { get; }

        public double Volume { get; private set; } = 0.5;
        public double Speed { get; private set; } = 0.5;
        public double Tone { get; private set; } = 0.5;
        public double Alpha { get; private set; } = 0.5;
        public double ToneScale { get; private set; } = 0.5;
        public double ComponentNormal { get; private set; } = 1.0;
        public double ComponentHappy { get; private set; }
        public double ComponentAnger { get; private set; }
        public double ComponentSorrow { get; private set; }
        public double ComponentCalmness { get; private set; }

        public IObservable<VoiceProfile> OnUpdate => _onUpdate;

        public void Dispose()
        {
            _onUpdate.Dispose();
        }

        public void Update(double volume, double speed, double tone, double alpha, double toneScale,
            double componentNormal, double componentHappy, double componentAnger, double componentSorrow,
            double componentCalmness)
        {
            if (volume is <0.0 or >1.0 ||
                speed is <0.0 or >1.0 ||
                tone is <0.0 or >1.0 ||
                alpha is <0.0 or >1.0 ||
                toneScale is <0.0 or >1.0 ||
                componentNormal is <0.0 or >1.0 ||
                componentHappy is <0.0 or >1.0 ||
                componentAnger is <0.0 or >1.0 ||
                componentSorrow is <0.0 or >1.0 ||
                componentCalmness is <0.0 or >1.0)
            {
                throw new AggregateException();
            }

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

            _onUpdate.OnNext(this);
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

        public class MastodonFollowNotificationVoiceProfile : VoiceProfile
        {
            public MastodonFollowNotificationVoiceProfile(AccountIdentifier accountIdentifier) : base(
                accountIdentifier)
            {
            }
        }

        public class MastodonFollowRequestNotificationVoiceProfile : VoiceProfile
        {
            public MastodonFollowRequestNotificationVoiceProfile(AccountIdentifier accountIdentifier) : base(
                accountIdentifier)
            {
            }
        }

        public class MastodonMentionNotificationVoiceProfile : VoiceProfile
        {
            public MastodonMentionNotificationVoiceProfile(AccountIdentifier accountIdentifier) : base(
                accountIdentifier)
            {
            }
        }

        public class MastodonSensitiveMentionNotificationVoiceProfile : VoiceProfile
        {
            public MastodonSensitiveMentionNotificationVoiceProfile(AccountIdentifier accountIdentifier) : base(
                accountIdentifier)
            {
            }
        }

        public class MastodonReblogNotificationVoiceProfile : VoiceProfile
        {
            public MastodonReblogNotificationVoiceProfile(AccountIdentifier accountIdentifier) : base(
                accountIdentifier)
            {
            }
        }

        public class MastodonSensitiveReblogNotificationVoiceProfile : VoiceProfile
        {
            public MastodonSensitiveReblogNotificationVoiceProfile(AccountIdentifier accountIdentifier) : base(
                accountIdentifier)
            {
            }
        }

        public class MastodonFavoriteNotificationVoiceProfile : VoiceProfile
        {
            public MastodonFavoriteNotificationVoiceProfile(AccountIdentifier accountIdentifier) : base(
                accountIdentifier)
            {
            }
        }

        public class MastodonSensitiveFavoriteNotificationVoiceProfile : VoiceProfile
        {
            public MastodonSensitiveFavoriteNotificationVoiceProfile(AccountIdentifier accountIdentifier) : base(
                accountIdentifier)
            {
            }
        }
    }
}
