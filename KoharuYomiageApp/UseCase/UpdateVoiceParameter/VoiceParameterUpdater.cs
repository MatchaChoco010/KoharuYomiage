using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.Repository;
using KoharuYomiageApp.UseCase.UpdateVoiceParameter.DataObjects;

namespace KoharuYomiageApp.UseCase.UpdateVoiceParameter
{
    public class VoiceParameterUpdater : IStartUpdatingVoiceParameter, IDisposable
    {
        readonly IGlobalVolumeRepository _globalVolumeRepository;
        readonly IInitializeGlobalVolumeView _initializeGlobalVolumeView;
        readonly IUpdateVoiceParameter _updateVoiceParameter;
        readonly IVoiceParameterChangeNotifierRepository _voiceParameterChangeNotifierRepository;

        IDisposable? _disposable;

        public VoiceParameterUpdater(IVoiceParameterChangeNotifierRepository voiceParameterChangeNotifierRepository,
            IGlobalVolumeRepository globalVolumeRepository, IUpdateVoiceParameter updateVoiceParameter,
            IInitializeGlobalVolumeView initializeGlobalVolumeView)
        {
            _voiceParameterChangeNotifierRepository = voiceParameterChangeNotifierRepository;
            _globalVolumeRepository = globalVolumeRepository;
            _updateVoiceParameter = updateVoiceParameter;
            _initializeGlobalVolumeView = initializeGlobalVolumeView;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            var globalVolume = await _globalVolumeRepository.GetGlobalVolume(cancellationToken);
            _initializeGlobalVolumeView.Initialize(globalVolume.Volume.Value);

            var notifier = await _voiceParameterChangeNotifierRepository.GetInstance(cancellationToken);

            _disposable = notifier.VoiceParameter.Subscribe(param =>
                _updateVoiceParameter.Update(new VoiceParameterData(param.Volume, param.Speed, param.Tone, param.Alpha,
                    param.ToneScale, param.ComponentNormal, param.ComponentHappy, param.ComponentAnger,
                    param.ComponentSorrow, param.ComponentCalmness)));
            cancellationToken.Register(Dispose);
        }
    }
}
