namespace KoharuYomiageApp.Application.LoadTalker.UseCases
{
    public class LoadTalkerInteractor : ILoadTalkerInputBoundary
    {
        readonly ILoadTalkerOutputBoundary _outputBoundary;

        public LoadTalkerInteractor(ILoadTalkerOutputBoundary outputBoundary)
        {
            _outputBoundary = outputBoundary;
        }

        public void HandleLoadedWindow()
        {
            _outputBoundary.CompleteLoadedWindow();
        }

        public void HandleLoadedTalker(LoadTalkerStatus status)
        {
            switch (status)
            {
                case LoadTalkerStatus.Success:
                    _outputBoundary.CompleteLoadedTalker();
                    break;
                case LoadTalkerStatus.Failure:
                    _outputBoundary.FailureLoadTalker();
                    break;
            }
        }
    }
}
