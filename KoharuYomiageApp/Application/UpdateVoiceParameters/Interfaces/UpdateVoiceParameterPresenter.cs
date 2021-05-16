using KoharuYomiageApp.Application.UpdateVoiceParameters.Interfaces.DataObjects;
using KoharuYomiageApp.Application.UpdateVoiceParameters.UseCases;
using KoharuYomiageApp.Application.UpdateVoiceParameters.UseCases.DataObjects;

namespace KoharuYomiageApp.Application.UpdateVoiceParameters.Interfaces
{
    public class UpdateVoiceParameterPresenter : IUpdateVoiceParameter
    {
        readonly ICeVIOAIUpdateVoiceParameterService _service;

        public UpdateVoiceParameterPresenter(ICeVIOAIUpdateVoiceParameterService service)
        {
            _service = service;
        }

        public void Update(VoiceParameterData data)
        {
            _service.Update(new VoiceParameterOutputData(data.Volume, data.Speed, data.Tone, data.Alpha, data.ToneScale,
                data.ComponentNormal, data.ComponentHappy, data.ComponentAnger, data.ComponentSorrow,
                data.ComponentCalmness));
        }
    }
}
