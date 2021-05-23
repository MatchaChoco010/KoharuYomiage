using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Presentation.CeVIOAI
{
    public interface ICeVIOAILoadTalker
    {
        Task LoadTalker(CancellationToken cancellationToken);
    }
}
