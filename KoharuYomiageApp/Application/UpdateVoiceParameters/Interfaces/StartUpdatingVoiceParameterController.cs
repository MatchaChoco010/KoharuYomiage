using KoharuYomiageApp.Application.UpdateVoiceParameters.UseCases;

namespace KoharuYomiageApp.Application.UpdateVoiceParameters.Interfaces
{
    public class StartUpdatingVoiceParameterController
    {
        readonly IStartUpdatingVoiceParameter _startUpdatingVoiceParameter;

        public StartUpdatingVoiceParameterController(IStartUpdatingVoiceParameter startUpdatingVoiceParameter)
        {
            _startUpdatingVoiceParameter = startUpdatingVoiceParameter;
        }

        public void Start()
        {
            _ = _startUpdatingVoiceParameter.Start();
        }
    }
}
