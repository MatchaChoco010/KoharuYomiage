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

        public void HandleLoadedTalker()
        {
            _outputBoundary.CompleteLoadedTalker();
        }
    }
}
