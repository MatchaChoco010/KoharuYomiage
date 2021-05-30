using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using KoharuYomiageApp.Infrastructures.GUI.Views.Dialogs;
using KoharuYomiageApp.Presentation.GUI;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using SourceChord.FluentWPF;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class AccountListViewModel : BindableBase, INavigationAware, IRegionMemberLifetime, IDisposable
    {
        readonly AccountListController _accountListController;
        readonly CompositeDisposable _disposable = new();
        readonly IDialogService _dialogService;
        readonly Subject<Unit> _refreshAccountListSubject = new();

        CancellationTokenSource? _cancellationTokenSource;

        public AccountListViewModel(AccountListController accountListController, IDialogService dialogService)
        {
            _accountListController = accountListController;
            _dialogService = dialogService;
        }

        public ReactiveCommand AddAccountCommand { get; } = new();
        public ReactiveCommand BackCommand { get; } = new();
        public ReactivePropertySlim<List<AccountItem>> AccountList { get; } = new(new List<AccountItem>());

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel(true);
            _cancellationTokenSource?.Dispose();
            _refreshAccountListSubject.Dispose();
            _disposable.Dispose();
            BackCommand.Dispose();
            AddAccountCommand.Dispose();

            foreach (var account in AccountList.Value)
            {
                account.Dispose();
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _ = ShowAllAccounts(_cancellationTokenSource.Token);

            _refreshAccountListSubject.SelectMany(_ => Observable.StartAsync(async cancellationToken =>
            {
                await ShowAllAccounts(cancellationToken);
            })).Subscribe().AddTo(_disposable);

            AddAccountCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(SelectSNS)))
                .AddTo(_disposable);
            BackCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(Setting)))
                .AddTo(_disposable);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            _cancellationTokenSource?.Cancel(true);
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            _disposable.Clear();

            foreach (var account in AccountList.Value)
            {
                account.Dispose();
            }

            AccountList.Value = new List<AccountItem>();
        }

        public bool KeepAlive => false;

        async Task ShowAllAccounts(CancellationToken cancellationToken)
        {
            foreach (var account in AccountList.Value)
            {
                account.Dispose();
            }

            var accounts = await _accountListController.GetAllAcounts(cancellationToken);
            AccountList.Value = accounts.Select(tuple =>
            {
                var (id, username, instance, iconUrl, isReadingPostFromThisAccount) = tuple;
                return new AccountItem(_accountListController, _dialogService, id, username, instance, iconUrl,
                    isReadingPostFromThisAccount, _refreshAccountListSubject);
            }).ToList();
        }

        public class AccountItem : IDisposable
        {
            readonly CompositeDisposable _disposable = new();

            public AccountItem(AccountListController controller, IDialogService dialogService, string accountIdentifier,
                string username, string instance, Uri iconUrl, bool isReadingPostsFromThisAccount,
                Subject<Unit> refreshAllAccountListSubject)
            {
                var isReadingFlag = isReadingPostsFromThisAccount;

                SwitchReadingButtonText = isReadingPostsFromThisAccount
                    ? new ReactivePropertySlim<string>("読上げ中")
                    : new ReactivePropertySlim<string>("読上げ停止中");
                SwitchButtonBackground = isReadingPostsFromThisAccount
                    ? new ReactivePropertySlim<Brush>(AccentColors.ImmersiveSystemAccentBrush)
                    : new ReactivePropertySlim<Brush>(Brushes.Gray);

                AccountIdentifier = accountIdentifier;
                Username = username;
                Instance = instance;
                IconUrl = iconUrl;

                SwitchReadingCommand.SelectMany(_ => Observable.StartAsync(async cancellationToken =>
                {
                    isReadingFlag = !isReadingFlag;
                    SwitchReadingButtonText.Value = isReadingFlag ? "読上げ中" : "読上げ停止中";
                    SwitchButtonBackground.Value =
                        isReadingFlag ? AccentColors.ImmersiveSystemAccentBrush : Brushes.Gray;
                    await controller.SwitchConnection(Username, Instance, isReadingFlag, cancellationToken);
                })).Subscribe().AddTo(_disposable);
                DeleteAccountCommand.SelectMany(_ => Observable.StartAsync(async cancellationToken =>
                {
                    TaskCompletionSource<IDialogResult> tcs = new();
                    cancellationToken.Register(() => tcs.TrySetCanceled());
                    dialogService.ShowDialog(nameof(AccountDeletionConfirmation),
                        new DialogParameters($"AccountIdentifier={AccountIdentifier}"),
                        r =>
                        {
                            tcs.SetResult(r);
                        });
                    var result = await tcs.Task;
                    if (result.Result == ButtonResult.OK)
                    {
                        await controller.DeleteAccount(Username, Instance, cancellationToken);
                        refreshAllAccountListSubject.OnNext(Unit.Default);
                    }
                })).Subscribe().AddTo(_disposable);
            }

            public string AccountIdentifier { get; }
            public string Username { get; }
            public string Instance { get; }
            public Uri IconUrl { get; }
            public ReactiveCommand SwitchReadingCommand { get; } = new();
            public ReactiveCommand DeleteAccountCommand { get; } = new();
            public ReactivePropertySlim<string> SwitchReadingButtonText { get; }
            public ReactivePropertySlim<Brush> SwitchButtonBackground { get; }

            public void Dispose()
            {
                _disposable.Dispose();
                SwitchReadingButtonText.Dispose();
                SwitchReadingCommand.Dispose();
                DeleteAccountCommand.Dispose();
            }
        }
    }
}
