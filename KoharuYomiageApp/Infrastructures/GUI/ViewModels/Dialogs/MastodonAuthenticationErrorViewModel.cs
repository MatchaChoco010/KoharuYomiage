using System;
using System.Reactive.Disposables;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels.Dialogs
{
    public class MastodonAuthenticationErrorViewModel : BindableBase, IDialogAware, IDisposable
    {
        readonly CompositeDisposable _disposable = new();

        public MastodonAuthenticationErrorViewModel()
        {
            OkCommand.Subscribe(_ => RequestClose?.Invoke(new DialogResult(ButtonResult.OK))).AddTo(_disposable);
        }

        public ReactiveCommand OkCommand { get; } = new();

        public event Action<IDialogResult>? RequestClose;

        public string Title => "";

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            // Do nothing
        }

        public void OnDialogClosed()
        {
            _disposable.Clear();
        }

        public void Dispose()
        {
            _disposable.Dispose();
            OkCommand.Dispose();
        }
    }
}
