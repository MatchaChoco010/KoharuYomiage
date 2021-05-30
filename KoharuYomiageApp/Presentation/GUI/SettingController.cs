using System;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.ReadingTextContainerSize;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class SettingController
    {
        readonly IChangeReadingTextContainerSize _changeReadingTextContainerSize;
        readonly IGetReadingTextContainerSize _getReadingTextContainerSize;

        public SettingController(IChangeReadingTextContainerSize changeReadingTextContainerSize,
            IGetReadingTextContainerSize getReadingTextContainerSize)
        {
            _changeReadingTextContainerSize = changeReadingTextContainerSize;
            _getReadingTextContainerSize = getReadingTextContainerSize;
        }

        public async Task ChangeReadingTextContainerSize(int size, CancellationToken cancellationToken)
        {
            await _changeReadingTextContainerSize.ChangeContainerSize(size, cancellationToken);
        }

        public async Task<int> GetReadingTextContainerSize(CancellationToken cancellationToken)
        {
            return await _getReadingTextContainerSize.GetReadingTextContainerSize(cancellationToken);
        }
    }
}
