using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.UseCase.Utils
{
    public interface ISpeakText
    {
        Task SpeakText(string text, CancellationToken cancellationToken);
    }
}
