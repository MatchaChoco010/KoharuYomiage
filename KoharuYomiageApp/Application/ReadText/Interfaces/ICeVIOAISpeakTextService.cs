using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.ReadText.Interfaces
{
    public interface ICeVIOAISpeakTextService
    {
        Task SpeakText(string text, CancellationToken cancellationToken);
    }
}
