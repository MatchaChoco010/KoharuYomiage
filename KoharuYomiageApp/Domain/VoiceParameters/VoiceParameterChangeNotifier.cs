using System;
using Reactive.Bindings;

namespace KoharuYomiageApp.Domain.VoiceParameters
{
    public class VoiceParameterChangeNotifier : IDisposable
    {
        readonly GlobalVolume _globalVolume;
        readonly IDisposable _globalVolumeDisposable;

        readonly ReactivePropertySlim<VoiceParameter> _voiceParameter;
        IDisposable? _currentProfileDisposable;
        VoiceProfile? _currentVoiceProfile;

        public VoiceParameterChangeNotifier(GlobalVolume globalVolume)
        {
            _globalVolume = globalVolume;

            _voiceParameter = new ReactivePropertySlim<VoiceParameter>(ConvertVoiceParameter());
            _globalVolumeDisposable = _globalVolume.Volume
                .Subscribe(_ =>
                {
                    _voiceParameter.Value = ConvertVoiceParameter();
                });
        }

        public IReadOnlyReactiveProperty<VoiceParameter> VoiceParameter => _voiceParameter;

        public void Dispose()
        {
            _currentProfileDisposable?.Dispose();
            _globalVolumeDisposable.Dispose();
        }

        public void SetCurrentProfile(VoiceProfile newVoiceProfile)
        {
            _currentProfileDisposable?.Dispose();

            _currentVoiceProfile = newVoiceProfile;
            _voiceParameter.Value = ConvertVoiceParameter();
            _currentProfileDisposable = _currentVoiceProfile.OnUpdate
                .Subscribe(profile =>
                {
                    _currentVoiceProfile = profile;
                    _voiceParameter.Value = ConvertVoiceParameter();
                });
        }

        VoiceParameter ConvertVoiceParameter()
        {
            var volume = (uint)((_currentVoiceProfile?.Volume ?? 0.5) * _globalVolume.GetMultiplier() * 100.0);
            var speed = (uint)((_currentVoiceProfile?.Speed ?? 0.5) * 100.0);
            var tone = (uint)((_currentVoiceProfile?.Tone ?? 0.5) * 100.0);
            var alpha = (uint)((_currentVoiceProfile?.Alpha ?? 0.5) * 100.0);
            var toneScale = (uint)((_currentVoiceProfile?.ToneScale ?? 0.5) * 100.0);
            var componentNormal = (uint)((_currentVoiceProfile?.ComponentNormal ?? 1.0) * 100.0);
            var componentHappy = (uint)((_currentVoiceProfile?.ComponentHappy ?? 0.0) * 100.0);
            var componentAnger = (uint)((_currentVoiceProfile?.ComponentAnger ?? 0.0) * 100.0);
            var componentSorrow = (uint)((_currentVoiceProfile?.ComponentSorrow ?? 0.0) * 100.0);
            var componentCalmness = (uint)((_currentVoiceProfile?.ComponentCalmness ?? 0.0) * 100.0);

            volume = volume is >100 ? 100 : volume;
            speed = speed is >100 ? 100 : speed;
            tone = tone is >100 ? 100 : tone;
            alpha = alpha is >100 ? 100 : alpha;
            toneScale = toneScale is >100 ? 100 : toneScale;
            componentNormal = componentNormal is >100 ? 100 : componentNormal;
            componentHappy = componentHappy is >100 ? 100 : componentHappy;
            componentAnger = componentAnger is >100 ? 100 : componentAnger;
            componentSorrow = componentSorrow is >100 ? 100 : componentSorrow;
            componentCalmness = componentCalmness is >100 ? 100 : componentCalmness;

            return new VoiceParameter(volume, speed, tone, alpha, toneScale, componentNormal, componentHappy,
                componentAnger, componentSorrow, componentCalmness);
        }
    }
}
