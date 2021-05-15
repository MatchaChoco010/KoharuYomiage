using System.Threading.Tasks;
using KoharuYomiageApp.Application.WindowLoaded.UseCases;

namespace KoharuYomiageApp.Application.WindowLoaded.Interfaces
{
    public class LoadTalkerPresenter : ILoadTalker
    {
        readonly ICeVIOAILoadTalkerService _ceVioAi;

        public LoadTalkerPresenter(ICeVIOAILoadTalkerService ceVioAi)
        {
            _ceVioAi = ceVioAi;
        }

        public async Task LoadTalker()
        {
            await _ceVioAi.LoadTalker();
        }
    }
}
