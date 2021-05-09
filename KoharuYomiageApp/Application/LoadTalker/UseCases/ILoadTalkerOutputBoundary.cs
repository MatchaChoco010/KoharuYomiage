namespace KoharuYomiageApp.Application.LoadTalker.UseCases
{
    public interface ILoadTalkerOutputBoundary
    {
        void CompleteLoadedWindow();
        void CompleteLoadedTalker();
    }
}
