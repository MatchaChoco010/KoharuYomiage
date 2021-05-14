using System.Windows;
using KoharuYomiageApp.Application.AddMastodonAccount.Interfaces;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;
using KoharuYomiageApp.Application.LoadTalker.Interfaces;
using KoharuYomiageApp.Application.LoadTalker.UseCases;
using KoharuYomiageApp.Application.Repositories.Interfaces;
using KoharuYomiageApp.Application.Repositories.UseCases;
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
            // GUI
            // Dialog
            containerRegistry.RegisterDialogWindow<DialogWindow>();
            containerRegistry.RegisterDialog<LoadTalkerErrorDialogContent>();
            containerRegistry.RegisterDialog<LoadTalkerLinkDialogContent>();
            containerRegistry.RegisterDialog<RegisterClientErrorDialogContent>();
            // Views
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
            containerRegistry.RegisterForNavigation<Start>();
            containerRegistry.RegisterForNavigation<SelectSNS>();
            containerRegistry.RegisterForNavigation<MastodonLogin>();
            containerRegistry.RegisterForNavigation<MastodonAuthCode>();

            // Application
            // LoadTalker Feature
            containerRegistry.RegisterSingleton<ILoadTalkerInputBoundary, LoadTalkerInteractor>();
            containerRegistry.RegisterManySingleton<LoadTalkerPresenter>(typeof(ILoadTalkerOutputBoundary),
                typeof(LoadTalkerPresenter));
            containerRegistry.RegisterSingleton<LoadTalkerController>();
            // AddMastodonAccount Feature
            containerRegistry.RegisterSingleton<ILoginMastodonAccount, LoginMastodonAccount>();
            containerRegistry.RegisterSingleton<LoginMastodonAccountController>();
            containerRegistry.RegisterSingleton<IRegisterClient, RegisterClientPresenter>();
            containerRegistry.RegisterManySingleton<ShowRegisterClientErrorPresenter>(typeof(IShowRegisterClientError),
                typeof(ShowRegisterClientErrorPresenter));
            containerRegistry.RegisterManySingleton<ShowAuthUrlPresenter>(typeof(IShowAuthUrl),
                typeof(ShowAuthUrlPresenter));
            // Repositories
            containerRegistry.RegisterSingleton<IMastodonClientRepository, MastodonClientRepository>();

            // Infrastructures
            // CeVIOAI
            containerRegistry.RegisterSingleton<CeVIOAIService>();
            Container.Resolve<CeVIOAIService>();
            // MastodonApi
            containerRegistry.RegisterManySingleton<MastodonApiService>(typeof(IMastodonApiRegisterClientService),
                typeof(MastodonApiService));
            Container.Resolve<MastodonApiService>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Container.GetContainer().Dispose();
        }
    }
}
