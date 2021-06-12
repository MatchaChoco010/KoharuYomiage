using System;
using System.Reactive;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMisskeyAccount;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MisskeyLoginPresenter: IWaitAuthorize, IShowAuthUrl, IShowRegisterClientError, IShowAuthorizeError
    {
        readonly Subject<Uri> _onShowAuthUrl = new();
        readonly Subject<Unit> _onShowRegisterClientError = new();
        readonly Subject<Unit> _onShowAuthorizeError = new();

        public IObservable<Uri> OnShowAuthUrl => _onShowAuthUrl;
        public IObservable<Unit> OnShowRegisterClientError => _onShowRegisterClientError;
        public IObservable<Unit> OnShowAuthorizeError => _onShowAuthorizeError;

        TaskCompletionSource<int>? _taskCompletionSource;

        public void FinishAuthorize()
        {
            _taskCompletionSource?.TrySetResult(0);
        }

        public async Task WaitAuthorize(CancellationToken cancellationToken)
        {
            _taskCompletionSource = new TaskCompletionSource<int>();
            cancellationToken.Register(() => _taskCompletionSource.TrySetCanceled());
            await _taskCompletionSource.Task;
        }

        public void ShowAuthUrl(Uri authorizationUrl)
        {
            _onShowAuthUrl.OnNext(authorizationUrl);
        }

        public void ShowRegisterClientError()
        {
            _onShowRegisterClientError.OnNext(Unit.Default);
        }

        public void ShowAuthorizeError()
        {
            _onShowAuthorizeError.OnNext(Unit.Default);
        }
    }
}
