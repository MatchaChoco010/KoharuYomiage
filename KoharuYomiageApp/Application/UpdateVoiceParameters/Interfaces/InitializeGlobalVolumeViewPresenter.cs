using System;
using System.Reactive.Subjects;
using KoharuYomiageApp.Application.UpdateVoiceParameters.UseCases;

namespace KoharuYomiageApp.Application.UpdateVoiceParameters.Interfaces
{
    public class InitializeGlobalVolumeViewPresenter : IInitializeGlobalVolumeView
    {
        readonly BehaviorSubject<double> _onInitializeGlobalVolumeView = new(0);
        public IObservable<double> OnInitializeGlobalVolumeView => _onInitializeGlobalVolumeView;

        public void Initialize(double volume)
        {
            _onInitializeGlobalVolumeView.OnNext(volume);
            _onInitializeGlobalVolumeView.OnCompleted();
        }
    }
}
