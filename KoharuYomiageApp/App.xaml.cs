using System.Windows;
using KoharuYomiageApp.Application.AddMastodonAccount.Interfaces;
using KoharuYomiageApp.Application.AddMastodonAccount.UseCases;
using KoharuYomiageApp.Application.Repositories.Interfaces;
using KoharuYomiageApp.Application.Repositories.UseCases;
using KoharuYomiageApp.Application.WindowLoaded.Interfaces;
using KoharuYomiageApp.Application.WindowLoaded.UseCases;
using KoharuYomiageApp.Infrastructures;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using KoharuYomiageApp.Infrastructures.JsonStorage;
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
            containerRegistry.RegisterDialog<GetMastodonAccountInfoErrorDialogContent>();
            containerRegistry.RegisterDialog<MastodonAuthenticationErrorDialogContent>();
            // Views
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
            containerRegistry.RegisterForNavigation<Start>();
            containerRegistry.RegisterForNavigation<SelectSNS>();
            containerRegistry.RegisterForNavigation<MastodonLogin>();
            containerRegistry.RegisterForNavigation<MastodonAuthCode>();

            // Application
            // LoadTalker Feature
            containerRegistry.RegisterSingleton<IWindowLoaded, TalkerInitializer>();
            containerRegistry.RegisterSingleton<WindowLoadedController>();
            containerRegistry.RegisterManySingleton<LoadTalkerPresenter>(typeof(ILoadTalker),
                typeof(LoadTalkerPresenter));
            containerRegistry.RegisterManySingleton<ShowLoadTalkerErrorPresenter>(typeof(IShowLoadTalkerError),
                typeof(ShowLoadTalkerErrorPresenter));
            containerRegistry.RegisterManySingleton<FinishLoadTalkerPresenter>(typeof(IFinishLoadTalker),
                typeof(FinishLoadTalkerPresenter));
            containerRegistry.RegisterSingleton<IPushStartButton, AccountExistenceChecker>();
            containerRegistry.RegisterSingleton<PushStartButtonController>();
            containerRegistry.RegisterManySingleton<StartRegisteringAccountPresenter>(typeof(IStartRegisteringAccount),
                typeof(StartRegisteringAccountPresenter));
            containerRegistry
                .RegisterManySingleton<Application.WindowLoaded.Interfaces.AddMastodonAccountToReaderPresenter>(
                    typeof(Application.WindowLoaded.UseCases.IAddMastodonAccountToReader),
                    typeof(Application.WindowLoaded.Interfaces.AddMastodonAccountToReaderPresenter));
            containerRegistry.RegisterManySingleton<StartAppPresenter>(typeof(IStartApp), typeof(StartAppPresenter));
            // AddMastodonAccount Feature
            containerRegistry.RegisterSingleton<ILoginMastodonAccount, LoginMastodonAccount>();
            containerRegistry.RegisterSingleton<LoginMastodonAccountController>();
            containerRegistry.RegisterSingleton<IRegisterClient, RegisterClientPresenter>();
            containerRegistry.RegisterManySingleton<ShowRegisterClientErrorPresenter>(typeof(IShowRegisterClientError),
                typeof(ShowRegisterClientErrorPresenter));
            containerRegistry.RegisterManySingleton<ShowAuthUrlPresenter>(typeof(IShowAuthUrl),
                typeof(ShowAuthUrlPresenter));
            containerRegistry.RegisterSingleton<IAuthorizeMastodonAccount, AuthorizeMastodonAccount>();
            containerRegistry.RegisterSingleton<AuthorizeMastodonAccountController>();
            containerRegistry.RegisterManySingleton<AuthorizeMastodonAccountWithCodePresenter>(
                typeof(IAuthorizeMastodonAccountWithCode),
                typeof(AuthorizeMastodonAccountWithCodePresenter));
            containerRegistry.RegisterManySingleton<ShowMastodonAuthenticationErrorPresenter>(
                typeof(IShowMastodonAuthenticationError),
                typeof(ShowMastodonAuthenticationErrorPresenter));
            containerRegistry.RegisterManySingleton<GetAccountInfoPresenter>(typeof(IGetAccountInfo),
                typeof(GetAccountInfoPresenter));
            containerRegistry.RegisterManySingleton<ShowGetMastodonAccountInfoErrorPresenter>(
                typeof(IShowGetMastodonAccountInfoError),
                typeof(ShowGetMastodonAccountInfoErrorPresenter));
            containerRegistry
                .RegisterManySingleton<Application.AddMastodonAccount.Interfaces.AddMastodonAccountToReaderPresenter>(
                    typeof(Application.AddMastodonAccount.UseCases.IAddMastodonAccountToReader),
                    typeof(Application.AddMastodonAccount.Interfaces.AddMastodonAccountToReaderPresenter));
            containerRegistry.RegisterManySingleton<FinishAuthorizeMastodonAccountPresenter>(
                typeof(IFinishAuthorizeMastodonAccount),
                typeof(FinishAuthorizeMastodonAccountPresenter));
            // Repositories
            containerRegistry.RegisterSingleton<MastodonAccountRepository>();
            containerRegistry.RegisterSingleton<IMastodonAccountGateway, MastodonAccountGateway>();
            containerRegistry.RegisterSingleton<MastodonClientRepository>();
            containerRegistry.RegisterSingleton<IMastodonClientGateway, MastodonClientGateway>();

            // Infrastructures
            // CeVIOAI
            containerRegistry.RegisterManySingleton<CeVIOAIService>(
                typeof(ICeVIOAILoadTalkerService),
                typeof(CeVIOAIService));
            Container.Resolve<CeVIOAIService>();
            // MastodonApi
            containerRegistry.RegisterManySingleton<MastodonApiService>(
                typeof(Application.WindowLoaded.Interfaces.IMastodonApiAddAccountToReaderService),
                typeof(Application.AddMastodonAccount.Interfaces.IMastodonApiAddAccountToReaderService),
                typeof(IMastodonApiAuthorizeAccountWithCodeService),
                typeof(IMastodonApiGetAccountInfoService),
                typeof(IMastodonApiRegisterClientService),
                typeof(MastodonApiService));
            Container.Resolve<MastodonApiService>();
            // JsonStorage
            containerRegistry.RegisterMany<JsonStorage>(
                typeof(IMastodonAccountStorage),
                typeof(IMastodonClientStorage),
                typeof(JsonStorage));
            Container.Resolve<JsonStorage>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Container.GetContainer().Dispose();
        }
    }
}
