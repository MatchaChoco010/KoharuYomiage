namespace KoharuYomiageApp.Application.LoadTalker.UseCases
{
    public interface ILoadTalkerInputBoundary
    {
        void HandleLoadedWindow();
        void HandleLoadedTalker(LoadTalkerStatus status);
    }
}
