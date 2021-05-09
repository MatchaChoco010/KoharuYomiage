using KoharuYomiageApp.Application.LoadTalker.UseCases;

namespace KoharuYomiageApp.Application.LoadTalker.Interfaces
{
    public class LoadTalkerController
    {
        readonly ILoadTalkerInputBoundary _inputBoundary;

        public LoadTalkerController(ILoadTalkerInputBoundary inputBoundary)
        {
            _inputBoundary = inputBoundary;
        }

        public void WindowLoaded()
        {
            _inputBoundary.HandleLoadedWindow();
        }

        public void TalkerLoadedSuccess()
        {
            _inputBoundary.HandleLoadedTalker(new LoadTalkerStatus.Success());
        }

        public void TalkerLoadedFailure()
        {
            _inputBoundary.HandleLoadedTalker(new LoadTalkerStatus.Failure());
        }
    }
}
