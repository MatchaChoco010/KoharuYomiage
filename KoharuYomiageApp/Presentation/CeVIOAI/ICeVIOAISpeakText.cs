using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Presentation.CeVIOAI
{
    public interface ICeVIOAISpeakText
    {
        Task SpeakText(string text, CancellationToken cancellationToken);
    }
}
