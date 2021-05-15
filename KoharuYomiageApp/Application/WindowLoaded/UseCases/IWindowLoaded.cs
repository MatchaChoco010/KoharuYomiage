using System.Threading.Tasks;

namespace KoharuYomiageApp.Application.WindowLoaded.UseCases
{
    public interface IWindowLoaded
    {
        ValueTask LoadedWindow();
    }
}
