﻿using System;
using System.Reactive.Disposables;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace KoharuYomiageApp.Application.ViewModels
{
    public class LoadTalkerLinkDialogContentViewModel : BindableBase, IDialogAware
    {
        readonly CompositeDisposable _disposable = new();

        public event Action<IDialogResult>? RequestClose;

        public string Title => "";

        public ReactiveCommand OkCommand { get; } = new();
        public ReactiveCommand<Uri> LinkCommand { get; } = new();

        public LoadTalkerLinkDialogContentViewModel()
        {
            OkCommand.Subscribe(_ => RequestClose?.Invoke(new DialogResult(ButtonResult.OK))).AddTo(_disposable);
            LinkCommand.Subscribe(uri => System.Diagnostics.Process.Start(uri.ToString())).AddTo(_disposable);
        }

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