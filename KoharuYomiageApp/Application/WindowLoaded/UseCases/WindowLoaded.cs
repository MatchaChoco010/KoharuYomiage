using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.WindowLoaded.UseCases
{
    public class WindowLoaded : IWindowLoaded
    {
        readonly ILoadTalker _loadTalker;
        readonly IShowLoadTalkerError _showLoadTalkerError;
        readonly IFinishLoadTalker _finishLoadTalker;

        public WindowLoaded(ILoadTalker loadTalker, IShowLoadTalkerError showLoadTalkerError, IFinishLoadTalker finishLoadTalker)
        {
            _loadTalker = loadTalker;
            _showLoadTalkerError = showLoadTalkerError;
            _finishLoadTalker = finishLoadTalker;
        }

        public async ValueTask LoadedWindow()
        {
            try
            {
                await _loadTalker.LoadTalker();
            }
            catch
            {
                _showLoadTalkerError.ShowLoadTalkerError();
                return;
            }

            _finishLoadTalker.FinishLoadTalker();
        }
    }
}
