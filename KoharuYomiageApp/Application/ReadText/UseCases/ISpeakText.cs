using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.ReadText.UseCases
{
    public interface ISpeakText
    {
        Task SpeakText(string text, CancellationToken cancellationToken);
    }
}
