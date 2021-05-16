using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Application.UpdateVoiceParameters.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.UpdateVoiceParameters.UseCases
{
    public class VoiceParameterUpdater : IStartUpdatingVoiceParameter, IDisposable
    {
        readonly IInitializeGlobalVolumeView _initializeGlobalVolumeView;
        readonly IUpdateVoiceParameter _updateVoiceParameter;
        readonly VoiceParameterChangeNotifierRepository _voiceParameterChangeNotifierRepository;

        IDisposable? _disposable;

        public VoiceParameterUpdater(VoiceParameterChangeNotifierRepository voiceParameterChangeNotifierRepository,
            IUpdateVoiceParameter updateVoiceParameter, IInitializeGlobalVolumeView initializeGlobalVolumeView)
        {
            _voiceParameterChangeNotifierRepository = voiceParameterChangeNotifierRepository;
            _updateVoiceParameter = updateVoiceParameter;
            _initializeGlobalVolumeView = initializeGlobalVolumeView;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public async Task Start()
        {
            var notifier = await _voiceParameterChangeNotifierRepository.GetInstance();
            notifier.VoiceParameter.Take(1).Subscribe(param => _initializeGlobalVolumeView.Initialize(param.Volume));
            _disposable = notifier.VoiceParameter.Subscribe(param =>
                _updateVoiceParameter.Update(new VoiceParameterData(param.Volume, param.Speed, param.Tone, param.Alpha,
                    param.ToneScale, param.ComponentNormal, param.ComponentHappy, param.ComponentAnger,
                    param.ComponentSorrow, param.ComponentCalmness)));
        }
    }
}
