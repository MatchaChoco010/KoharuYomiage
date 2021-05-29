using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.Repository;
using KoharuYomiageApp.UseCase.UpdateVoiceParameter.DataObjects;

namespace KoharuYomiageApp.UseCase.UpdateVoiceParameter
{
    public class VoiceParameterUpdater : IStartUpdatingVoiceParameter, IDisposable
    {
        readonly IUpdateVoiceParameter _updateVoiceParameter;
        readonly IVoiceParameterChangeNotifierRepository _voiceParameterChangeNotifierRepository;

        IDisposable? _disposable;

        public VoiceParameterUpdater(IVoiceParameterChangeNotifierRepository voiceParameterChangeNotifierRepository,
            IUpdateVoiceParameter updateVoiceParameter)
        {
            _voiceParameterChangeNotifierRepository = voiceParameterChangeNotifierRepository;
            _updateVoiceParameter = updateVoiceParameter;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public async Task Start(CancellationToken cancellationToken)
        {
            var notifier = await _voiceParameterChangeNotifierRepository.GetInstance(cancellationToken);

            _disposable = notifier.VoiceParameter.Subscribe(param =>
                _updateVoiceParameter.Update(new VoiceParameterData(param.Volume, param.Speed, param.Tone, param.Alpha,
                    param.ToneScale, param.ComponentNormal, param.ComponentHappy, param.ComponentAnger,
                    param.ComponentSorrow, param.ComponentCalmness)));
            cancellationToken.Register(Dispose);
        }
    }
}
