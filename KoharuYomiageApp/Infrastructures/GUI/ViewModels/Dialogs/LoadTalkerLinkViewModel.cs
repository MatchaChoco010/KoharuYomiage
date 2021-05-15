using System;
using System.Diagnostics;
using System.Reactive.Disposables;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Infrastructures.GUI.ViewModels.Dialogs
{
    public class LoadTalkerLinkViewModel : BindableBase, IDialogAware
    {
        readonly CompositeDisposable _disposable = new();

        public LoadTalkerLinkViewModel()
        {
            OkCommand.Subscribe(_ => RequestClose?.Invoke(new DialogResult(ButtonResult.OK))).AddTo(_disposable);
            LinkCommand.Subscribe(uri => Process.Start(uri.ToString())).AddTo(_disposable);
        }

        public ReactiveCommand OkCommand { get; } = new();
        public ReactiveCommand<Uri> LinkCommand { get; } = new();

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
    }
}
