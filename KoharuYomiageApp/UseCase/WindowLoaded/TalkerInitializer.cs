using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.WindowLoaded
{
    public class TalkerInitializer : IWindowLoaded
    {
        readonly IFinishLoadTalker _finishLoadTalker;
        readonly ILoadTalker _loadTalker;
        readonly IShowLoadTalkerError _showLoadTalkerError;

        public TalkerInitializer(ILoadTalker loadTalker, IShowLoadTalkerError showLoadTalkerError,
            IFinishLoadTalker finishLoadTalker)
        {
            _loadTalker = loadTalker;
            _showLoadTalkerError = showLoadTalkerError;
            _finishLoadTalker = finishLoadTalker;
        }

        public async Task WindowLoaded(CancellationToken cancellationToken)
        {
            try
            {
                await _loadTalker.LoadTalker(cancellationToken);
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
