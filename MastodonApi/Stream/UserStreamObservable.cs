﻿using System;
using MastodonApi.Payloads;

namespace MastodonApi.Stream
{
    internal class UserStreamObservable : IObservable<UserStreamPayload>
    {
        readonly string _hostName;
        readonly AccessToken _accessToken;

        internal UserStreamObservable(string hostName, AccessToken accessToken)
        {
            _hostName = hostName;
            _accessToken = accessToken;
        }

        IDisposable IObservable<UserStreamPayload>.Subscribe(IObserver<UserStreamPayload> observer)
        {
            var connection = new UserStreamConnection();
            _ = connection.Start(observer, _hostName, _accessToken);
            return connection;
        }
    }
}
