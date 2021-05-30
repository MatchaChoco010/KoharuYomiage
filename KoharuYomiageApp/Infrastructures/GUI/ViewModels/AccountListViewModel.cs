using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using KoharuYomiageApp.Infrastructures.GUI.Views;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels
{
    public class AccountListViewModel : BindableBase, INavigationAware, IRegionMemberLifetime, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        public ReactiveCommand AddAccountCommand { get; } = new();
        public ReactiveCommand BackCommand { get; } = new();
        public ReactivePropertySlim<List<AccountItem>> AccountList { get; } = new();

        public void Dispose()
        {
            _disposable.Dispose();
            BackCommand.Dispose();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
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
            _disposable.Clear();
        }

        public bool KeepAlive => false;

        public record AccountItem(string DisplayName, string Instance, Uri IconUrl);
    }
}
