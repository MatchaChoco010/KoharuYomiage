using System;
using System.Reactive;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using KoharuYomiageApp.UseCase.AddMisskeyAccount;

namespace KoharuYomiageApp.Presentation.GUI
{
    public class MisskeyLoginController
    {
        readonly IAddMisskeyAccount _addMisskeyAccount;

        public MisskeyLoginController(IAddMisskeyAccount addMisskeyAccount)
        {
            _addMisskeyAccount = addMisskeyAccount;
        }

        public async Task LoginMisskeyAccount(string instanceName, CancellationToken cancellationToken)
        {
            await _addMisskeyAccount.Login(instanceName, cancellationToken);
        }
    }
}
