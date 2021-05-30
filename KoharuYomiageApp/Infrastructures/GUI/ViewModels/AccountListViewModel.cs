using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using KoharuYomiageApp.Presentation.GUI;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class AccountListViewModel : BindableBase, INavigationAware, IRegionMemberLifetime, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        readonly AccountListController _accountListController;

        public ReactiveCommand AddAccountCommand { get; } = new();
        public ReactiveCommand BackCommand { get; } = new();
        public ReactivePropertySlim<List<AccountItem>> AccountList { get; } = new();

        CancellationTokenSource? _cancellationTokenSource;

        public AccountListViewModel(AccountListController accountListController)
        {
            _accountListController = accountListController;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel(true);
            _cancellationTokenSource?.Dispose();
            _disposable.Dispose();
            BackCommand.Dispose();
        }

        async Task ShowAllAccounts(CancellationToken cancellationToken)
        {
            var accounts = await _accountListController.GetAllAcounts(cancellationToken);
            AccountList.Value = accounts.Select(tuple =>
            {
                var (id, username, instance, iconUrl) = tuple;
                return new AccountItem(username, instance, iconUrl);
            }).ToList();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _ = ShowAllAccounts(_cancellationTokenSource.Token);

            AddAccountCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(SelectSNS)))
                .AddTo(_disposable);
            BackCommand.Subscribe(_ => navigationContext.NavigationService.RequestNavigate(nameof(Setting)))
                .AddTo(_disposable);
            AccountList.Value = new List<AccountItem>
            {
                new("MatchaChoco010", "@social.orito-itsuki.net", new Uri(
                    "https://media.social.orito-itsuki.net/accounts/avatars/000/002/550/original/77be352262b9b405.png")),
                new("MatchaChoco010", "@social.orito-itsuki.net", new Uri(
                    "https://media.social.orito-itsuki.net/accounts/avatars/000/002/550/original/77be352262b9b405.png")),
            };
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
        }

        public bool KeepAlive => false;

        public class AccountItem
        {
            public string Username { get; }
            public string Instance { get; }
            public Uri IconUrl { get; }

            public AccountItem(string username, string instance, Uri iconUrl)
            {
                Username = username;
                Instance = instance;
                IconUrl = iconUrl;
            }
        }
    }
}
