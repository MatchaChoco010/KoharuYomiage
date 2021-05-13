using System.Windows;
using KoharuYomiageApp.Application.LoadTalker.Interfaces;
using KoharuYomiageApp.Application.LoadTalker.UseCases;
using KoharuYomiageApp.Infrastructures;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.DryIoc;
using Prism.Ioc;

namespace KoharuYomiageApp
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Dialog
            containerRegistry.RegisterDialogWindow<DialogWindow>();
            containerRegistry.RegisterDialog<LoadTalkerErrorDialogContent>();
            containerRegistry.RegisterDialog<LoadTalkerLinkDialogContent>();

            // Views
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
            containerRegistry.RegisterForNavigation<Start>();
            containerRegistry.RegisterForNavigation<SelectSNS>();
            containerRegistry.RegisterForNavigation<MastodonLogin>();

            // LoadTalker Feature
            containerRegistry.RegisterSingleton<ILoadTalkerInputBoundary, LoadTalkerInteractor>();
            containerRegistry.RegisterManySingleton<LoadTalkerPresenter>(typeof(ILoadTalkerOutputBoundary),
                typeof(LoadTalkerPresenter));
            containerRegistry.RegisterSingleton<LoadTalkerController>();

            // Infrastructures
            containerRegistry.RegisterSingleton<CeVIOAIService>();
            Container.Resolve<CeVIOAIService>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Container.GetContainer().Dispose();
        }
    }
}
