using System;
using System.Windows;
using KoharuYomiageApp.Data.JsonStorage;
using KoharuYomiageApp.Data.Repository;
using KoharuYomiageApp.Infrastructures.CeVIOAI;
using KoharuYomiageApp.Infrastructures.GUI.ViewModels;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using KoharuYomiageApp.Infrastructures.GUI.Views.Dialogs;
using KoharuYomiageApp.Infrastructures.Mastodon;
using KoharuYomiageApp.Presentation.CeVIOAI;
using KoharuYomiageApp.Presentation.GUI;
using KoharuYomiageApp.Presentation.Mastodon;
using KoharuYomiageApp.UseCase.AddMastodonAccount;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem;
using KoharuYomiageApp.UseCase.ReadText;
using KoharuYomiageApp.UseCase.Repository;
using KoharuYomiageApp.UseCase.UpdateVoiceParameter;
using KoharuYomiageApp.UseCase.WindowLoaded;
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
            // Infrastructures
            //   GUI
            //     Dialog
            containerRegistry.RegisterDialogWindow<DialogWindow>();
            containerRegistry.RegisterDialog<LoadTalkerError>();
            containerRegistry.RegisterDialog<LoadTalkerLink>();
            containerRegistry.RegisterDialog<RegisterClientError>();
            containerRegistry.RegisterDialog<GetMastodonAccountInfoError>();
            containerRegistry.RegisterDialog<MastodonAuthenticationError>();
            //     Views
            containerRegistry.RegisterForNavigation<ViewA>();
            containerRegistry.RegisterForNavigation<ViewB>();
            containerRegistry.RegisterForNavigation<Start>();
            containerRegistry.Register<StartViewModel>();
            containerRegistry.RegisterForNavigation<SelectSNS>();
            containerRegistry.RegisterForNavigation<MastodonLogin>();
            containerRegistry.RegisterForNavigation<MastodonAuthCode>();
            containerRegistry.RegisterForNavigation<MainControl>();
            //   CeVIOAI
            containerRegistry.RegisterManySingleton<CeVIOAIHost>(
                typeof(IDisposable),
                typeof(ICeVIOAILoadTalker),
                typeof(ICeVIOAISpeakText),
                typeof(ICeVIOAIUpdateVoiceParameter),
                typeof(CeVIOAIHost));
            //   MastodonApi
            containerRegistry.RegisterManySingleton<MastodonClient>(
                typeof(IMastodonAddAccountToReader),
                typeof(IMastodonAuthorizeAccountWithCode),
                typeof(IMastodonGetAccountInfo),
                typeof(IMastodonRegisterClient),
                typeof(MastodonClient));

            // Presentation
            //   GUI
            containerRegistry.RegisterManySingleton<StartPresenter>(
                typeof(StartPresenter),
                typeof(IStartApp),
                typeof(IStartRegisteringAccount),
                typeof(IFinishLoadTalker),
                typeof(IShowLoadTalkerError));
            containerRegistry.RegisterSingleton<StartController>();
            containerRegistry.RegisterManySingleton<MastodonLoginPresenter>(
                typeof(MastodonLoginPresenter),
                typeof(IShowAuthUrl),
                typeof(IShowRegisterClientError));
            containerRegistry.RegisterSingleton<MastodonLoginController>();
            containerRegistry.RegisterManySingleton<MastodonAuthCodePresenter>(
                typeof(MastodonAuthCodePresenter),
                typeof(IFinishAuthorizeMastodonAccount),
                typeof(IShowGetMastodonAccountInfoError),
                typeof(IShowMastodonAuthenticationError));
            containerRegistry.RegisterSingleton<MastodonAuthCodeController>();
            containerRegistry.RegisterManySingleton<MainControlPresenter>(
                typeof(MainControlPresenter),
                typeof(IInitializeGlobalVolumeView),
                typeof(IChangeImage),
                typeof(IUpdateTextListView));
            containerRegistry.RegisterManySingleton<MainControlController>(
                typeof(MainControlController),
                typeof(IDisposable));
            //   CeVIOAI
            containerRegistry.RegisterManySingleton<CeVIOAIPresenter>(
                typeof(ILoadTalker),
                typeof(ISpeakText),
                typeof(IUpdateVoiceParameter));
            //   Mastodon
            containerRegistry.RegisterManySingleton<MastodonPresenter>(
                typeof(MastodonPresenter),
                typeof(IRegisterClient),
                typeof(IAuthorizeMastodonAccountWithCode),
                typeof(IGetAccountInfo),
                typeof(UseCase.AddMastodonAccount.IAddMastodonAccountToReader),
                typeof(UseCase.WindowLoaded.IAddMastodonAccountToReader));
            containerRegistry.RegisterSingleton<MastodonController>();

            // Data
            //   Repository
            containerRegistry.RegisterSingleton<IGlobalVolumeRepository, GlobalVolumeRepository>();
            containerRegistry.RegisterSingleton<IMastodonAccountRepository, MastodonAccountRepository>();
            containerRegistry.RegisterSingleton<IMastodonClientRepository, MastodonClientRepository>();
            containerRegistry.RegisterSingleton<IReadingTextContainerRepository, ReadingTextContainerRepository>();
            containerRegistry
                .RegisterSingleton<IVoiceParameterChangeNotifierRepository, VoiceParameterChangeNotifierRepository>();
            containerRegistry.RegisterSingleton<IVoiceProfileRepository, VoiceProfileRepository>();
            //   JsonStorage
            containerRegistry.RegisterManySingleton<JsonStorage>(
                typeof(IMastodonAccountStorage),
                typeof(IMastodonClientStorage),
                typeof(IGlobalVolumeStorage),
                typeof(IVoiceProfileStorage),
                typeof(JsonStorage));

            // UseCase
            //   WindowLoaded
            containerRegistry.RegisterSingleton<IPushStartButton, AccountExistenceChecker>();
            containerRegistry.RegisterSingleton<IWindowLoaded, TalkerInitializer>();
            //   AddMastodonAccount
            containerRegistry.RegisterSingleton<IAuthorizeMastodonAccount, AuthorizeMastodonAccount>();
            containerRegistry.RegisterSingleton<ILoginMastodonAccount, LoginMastodonAccount>();
            //   AddMastodonTimelineItem
            containerRegistry.RegisterSingleton<IMastodonStatusReceiver, MastodonStatusReceiver>();
            containerRegistry.RegisterSingleton<IMastodonSensitiveStatusReceiver, MastodonSensitiveStatusReceiver>();
            containerRegistry.RegisterSingleton<IMastodonBoostedStatusReceiver, MastodonBoostedStatusReceiver>();
            containerRegistry
                .RegisterSingleton<IMastodonBoostedSensitiveStatusReceiver, MastodonBoostedSensitiveStatusReceiver>();
            //   ReadText
            containerRegistry.RegisterSingleton<IStartReading, TextReader>();
            //   UpdateVoiceParameter
            containerRegistry.RegisterSingleton<IUpdateGlobalVolume, GlobalVolumeUpdater>();
            containerRegistry.RegisterSingleton<IStartUpdatingVoiceParameter, VoiceParameterUpdater>();

            // Instantiate Infrastructure Instance
            Container.Resolve<CeVIOAIHost>();
            Container.Resolve<MastodonClient>();
            Container.Resolve<JsonStorage>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Container.GetContainer().Dispose();
        }
    }
}
