using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels.Dialogs
{
    public class AccountDeletionConfirmationViewModel : BindableBase, IDialogAware, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        public AccountDeletionConfirmationViewModel()
        {
            OkCommand.Subscribe(_ => RequestClose?.Invoke(new DialogResult(ButtonResult.OK))).AddTo(_disposable);
            CancelCommand.Subscribe(_ => RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel))).AddTo(_disposable);
        }

        public ReactiveCommand OkCommand { get; } = new();
        public ReactiveCommand CancelCommand { get; } = new();
        public ReactivePropertySlim<string> AccountIdentifier { get; } = new();

        public event Action<IDialogResult>? RequestClose;

        public string Title => "";

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            AccountIdentifier.Value = parameters.GetValue<string>("AccountIdentifier");
        }

        public void OnDialogClosed()
        {
            _disposable.Clear();
        }

        public void Dispose()
        {
            _disposable.Dispose();
            OkCommand.Dispose();
            CancelCommand.Dispose();
        }
    }
}
