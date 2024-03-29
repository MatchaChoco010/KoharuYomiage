﻿using System;
using System.Threading;
using System.Windows;
using KoharuYomiageApp.Data.JsonStorage;
using KoharuYomiageApp.Data.Repository;
using KoharuYomiageApp.Infrastructures.CeVIOAI;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using KoharuYomiageApp.Infrastructures.GUI.Views.AttachedBehavior;
using KoharuYomiageApp.Infrastructures.GUI.Views.Dialogs;
using KoharuYomiageApp.Infrastructures.Mastodon;
using KoharuYomiageApp.Infrastructures.Misskey;
using KoharuYomiageApp.Presentation.CeVIOAI;
using KoharuYomiageApp.Presentation.GUI;
using KoharuYomiageApp.Presentation.Mastodon;
using KoharuYomiageApp.Presentation.Misskey;
using KoharuYomiageApp.UseCase.AddMastodonAccount;
using KoharuYomiageApp.UseCase.AddMastodonTimelineItem;
using KoharuYomiageApp.UseCase.AddMisskeyAccount;
using KoharuYomiageApp.UseCase.AddMisskeyTimelineItem;
using KoharuYomiageApp.UseCase.DeleteAccount;
using KoharuYomiageApp.UseCase.EditVoiceProfile;
using KoharuYomiageApp.UseCase.GetAllAccounts;
using KoharuYomiageApp.UseCase.GetGlobalVolume;
using KoharuYomiageApp.UseCase.ReadingTextContainerSize;
using KoharuYomiageApp.UseCase.ReadText;
using KoharuYomiageApp.UseCase.Repository;
using KoharuYomiageApp.UseCase.SwitchAccountConnection;
using KoharuYomiageApp.UseCase.UpdateTextList;
using KoharuYomiageApp.UseCase.UpdateVoiceParameter;
using KoharuYomiageApp.UseCase.Utils;
using KoharuYomiageApp.UseCase.WindowLoaded;
using Microsoft.Windows.Sdk;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;

namespace KoharuYomiageApp
{
    public partial class App
    {
        static Mutex? _mutex;

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
            containerRegistry.RegisterDialog<MisskeyAuthenticationError>();
            containerRegistry.RegisterDialog<AccountDeletionConfirmation>();
            //     Views
            containerRegistry.RegisterForNavigation<Start>();
            containerRegistry.RegisterForNavigation<SelectSNS>();
            containerRegistry.RegisterForNavigation<MastodonLogin>();
            containerRegistry.RegisterForNavigation<MastodonAuthCode>();
            containerRegistry.RegisterForNavigation<MisskeyLogin>();
            containerRegistry.RegisterForNavigation<MainControl>();
            containerRegistry.RegisterForNavigation<Setting>();
            containerRegistry.RegisterForNavigation<License>();
            containerRegistry.RegisterForNavigation<AccountList>();
            containerRegistry.RegisterForNavigation<MastodonAccountSetting>();
            containerRegistry.RegisterForNavigation<MisskeyAccountSetting>();
            //   CeVIOAI
            containerRegistry.RegisterManySingleton<CeVIOAIHost>(
                typeof(IDisposable),
                typeof(ICeVIOAILoadTalker),
                typeof(ICeVIOAISpeakText),
                typeof(ICeVIOAIUpdateVoiceParameter),
                typeof(CeVIOAIHost));
            //   MastodonClient
            containerRegistry.RegisterManySingleton<MastodonClient>(
                typeof(Presentation.Mastodon.IMakeMastodonConnection),
                typeof(IMastodonAuthorizeAccountWithCode),
                typeof(IMastodonGetAccountInfo),
                typeof(IMastodonRegisterClient),
                typeof(MastodonClient));
            //   MisskeyClient
            containerRegistry.RegisterManySingleton<MisskeyClient>(
                typeof(Presentation.Misskey.IMakeMisskeyConnection),
                typeof(IMisskeyGetAuthorizeUrl),
                typeof(IMisskeyGetAccessToken),
                typeof(MisskeyClient));

            // Presentation
            //   GUI
            containerRegistry.RegisterManySingleton<StartPresenter>(
                typeof(StartPresenter),
                typeof(IStartApp),
                typeof(IStartRegisteringAccount),
                typeof(IFinishLoadTalker),
                typeof(IShowLoadTalkerError));
            containerRegistry.RegisterManySingleton<StartController>(
                typeof(StartController),
                typeof(IDisposable));
            containerRegistry.RegisterManySingleton<MastodonLoginPresenter>(
                typeof(MastodonLoginPresenter),
                typeof(UseCase.AddMastodonAccount.IShowAuthUrl),
                typeof(UseCase.AddMastodonAccount.IShowRegisterClientError));
            containerRegistry.RegisterManySingleton<MastodonLoginController>(
                typeof(MastodonLoginController),
                typeof(IDisposable));
            containerRegistry.RegisterManySingleton<MastodonAuthCodePresenter>(
                typeof(MastodonAuthCodePresenter),
                typeof(IFinishAuthorizeMastodonAccount),
                typeof(IShowGetMastodonAccountInfoError),
                typeof(IShowMastodonAuthenticationError));
            containerRegistry.RegisterManySingleton<MastodonAuthCodeController>(
                typeof(MastodonAuthCodeController),
                typeof(IDisposable));
            containerRegistry.RegisterManySingleton<MisskeyLoginPresenter>(
                typeof(MisskeyLoginPresenter),
                typeof(IWaitAuthorize),
                typeof(UseCase.AddMisskeyAccount.IShowAuthUrl),
                typeof(UseCase.AddMisskeyAccount.IShowRegisterClientError),
                typeof(IShowAuthorizeError));
            containerRegistry.RegisterSingleton<MisskeyLoginController>();
            containerRegistry.RegisterManySingleton<MainControlPresenter>(
                typeof(MainControlPresenter),
                typeof(IChangeImage),
                typeof(IUpdateTextListView));
            containerRegistry.RegisterManySingleton<MainControlController>(
                typeof(MainControlController),
                typeof(IDisposable));
            containerRegistry.RegisterSingleton<SettingController>();
            containerRegistry.RegisterSingleton<AccountListController>();
            containerRegistry.RegisterSingleton<MastodonAccountSettingController>();
            //   CeVIOAI
            containerRegistry.RegisterManySingleton<CeVIOAIPresenter>(
                typeof(ILoadTalker),
                typeof(ISpeakText),
                typeof(IUpdateVoiceParameter));
            //   Mastodon
            containerRegistry.RegisterManySingleton<MastodonPresenter>(
                typeof(UseCase.AddMastodonAccount.IRegisterClient),
                typeof(IAuthorizeMastodonAccountWithCode),
                typeof(IGetAccountInfo),
                typeof(UseCase.Utils.IMakeMastodonConnection));
            containerRegistry.RegisterSingleton<MastodonController>();
            //   Misskey
            containerRegistry.RegisterManySingleton<MisskeyPresenter>(
                typeof(IGetAccessToken),
                typeof(IGetAuthorizeUrl),
                typeof(UseCase.Utils.IMakeMisskeyConnection));
            containerRegistry.RegisterSingleton<MisskeyController>();

            // Data
            //   Repository
            containerRegistry.RegisterManySingleton<GlobalVolumeRepository>(
                typeof(IDisposable),
                typeof(IGlobalVolumeRepository));
            containerRegistry.RegisterSingleton<IMastodonAccountRepository, MastodonAccountRepository>();
            containerRegistry.RegisterSingleton<IMastodonClientRepository, MastodonClientRepository>();
            containerRegistry.RegisterSingleton<IMisskeyAccountRepository, MisskeyAccountRepository>();
            containerRegistry.RegisterManySingleton<ReadingTextContainerRepository>(
                typeof(IDisposable),
                typeof(IReadingTextContainerRepository));
            containerRegistry.RegisterManySingleton<VoiceParameterChangeNotifierRepository>(
                typeof(IDisposable),
                typeof(IVoiceParameterChangeNotifierRepository));
            containerRegistry.RegisterManySingleton<VoiceProfileRepository>(
                typeof(IDisposable),
                typeof(IVoiceProfileRepository));
            containerRegistry.RegisterManySingleton<ConnectionRepository>(
                typeof(IDisposable),
                typeof(IConnectionRepository));
            //   JsonStorage
            containerRegistry.RegisterManySingleton<JsonStorage>(
                typeof(IMastodonAccountStorage),
                typeof(IMastodonClientStorage),
                typeof(IMisskeyAccountStorage),
                typeof(IGlobalVolumeStorage),
                typeof(IVoiceProfileStorage),
                typeof(IReadingTextContainerStorage),
                typeof(JsonStorage));

            // UseCase
            //   WindowLoaded
            containerRegistry.RegisterSingleton<IPushStartButton, AccountExistenceChecker>();
            containerRegistry.RegisterSingleton<IWindowLoaded, TalkerInitializer>();
            //   AddMastodonAccount
            containerRegistry.RegisterSingleton<IAuthorizeMastodonAccount, AuthorizeMastodonAccount>();
            containerRegistry.RegisterSingleton<ILoginMastodonAccount, LoginMastodonAccount>();
            //   AddMisskeyAccount
            containerRegistry.RegisterSingleton<IAddMisskeyAccount, AddMisskeyAccount>();
            //   AddMastodonTimelineItem
            containerRegistry.RegisterSingleton<IMastodonTimelineItemReceiver, MastodonTimelineItemReceiver>();
            //   AddMisskeyTimelineItem
            containerRegistry.RegisterSingleton<IMisskeyTimelineItemReceiver, MisskeyTimelineItemReceiver>();
            //   GetGlobalVolume
            containerRegistry.RegisterSingleton<IGetGlobalVolume, GlobalVolumeProvider>();
            //   ReadingTextContainerSize
            containerRegistry.RegisterSingleton<IChangeReadingTextContainerSize, ReadingTextContainerSizeChanger>();
            containerRegistry.RegisterSingleton<IGetReadingTextContainerSize, ReadingTextContainerSizeProvider>();
            containerRegistry
                .RegisterSingleton<IInitializeReadingTextContainerSize, ReadingTextContainerSizeInitializer>();
            //   ReadText
            containerRegistry.RegisterSingleton<IStartReading, TextReader>();
            //   UpdateTextList
            containerRegistry.RegisterSingleton<IStartUpdatingTextList, TextListUpdater>();
            //   UpdateVoiceParameter
            containerRegistry.RegisterSingleton<IUpdateGlobalVolume, GlobalVolumeUpdater>();
            containerRegistry.RegisterSingleton<IStartUpdatingVoiceParameter, VoiceParameterUpdater>();
            //   GetAllAccounts
            containerRegistry.RegisterSingleton<IGetAllAccounts, AllAccountsProvider>();
            //   SwitchAccountConnection
            containerRegistry.RegisterSingleton<ISwitchAccountConnection, AccountConnectionSwitcher>();
            //   DeleteAccount
            containerRegistry.RegisterSingleton<IDeleteAccount, AccountDeleter>();
            //   EditVoiceProfile
            containerRegistry.RegisterSingleton<IVoiceProfileUpdater, VoiceProfileUpdater>();

            // Instantiate Infrastructure Instance
            Container.Resolve<CeVIOAIHost>();
            Container.Resolve<MastodonClient>();
            Container.Resolve<JsonStorage>();
        }

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            regionBehaviors.AddIfMissing(nameof(DisposeClosedViewsBehavior), typeof(DisposeClosedViewsBehavior));
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _mutex = new Mutex(false, "KoharuYomiageApp-{554C9FB9-3059-48C9-8606-055E26BFF3E5}");
            if (!_mutex.WaitOne(0, false))
            {
                var window = PInvoke.FindWindow(null, "小春六花さんにTLを読み上げていただくアプリ");
                PInvoke.ShowWindow(window, SHOW_WINDOW_CMD.SW_NORMAL);
                PInvoke.SetForegroundWindow(window);
                _mutex.Close();
                _mutex = null;
                Shutdown();
                return;
            }

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Container.GetContainer().Dispose();

            if (_mutex == null)
            {
                return;
            }

            _mutex.ReleaseMutex();
            _mutex.Close();
            _mutex = null;
        }
    }
}
