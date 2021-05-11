using Prism.Mvvm;
using Prism.Regions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
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
