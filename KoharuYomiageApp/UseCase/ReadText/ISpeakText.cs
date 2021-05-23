using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.ReadText
{
    public interface ISpeakText
    {
        Task SpeakText(string text, CancellationToken cancellationToken);
    }
}
