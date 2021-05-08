using Prism.Mvvm;
using Prism.Regions;

namespace KoharuYomiageApp.Interfaces.ViewModels
{
    public class ViewBViewModel : BindableBase, INavigationAware
    {
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
