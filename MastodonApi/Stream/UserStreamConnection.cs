using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MastodonApi.Payloads;
using MastodonApi.Payloads.Entities;

namespace MastodonApi.Stream
{
    internal class UserStreamConnection : IDisposable
    {
        readonly CancellationTokenSource _cancellationTokenSource = new();
        ClientWebSocket? _socket = null;

        public async ValueTask Start(IObserver<UserStreamPayload> observer, string hostName, AccessToken accessToken)
        {
            var cancellationToken = _cancellationTokenSource.Token;

            var instance = await Api.GetInstanceInformation(hostName);
            var streamingUrl = instance.urls["streaming_api"]!;

            var parameters = new List<KeyValuePair<string, string>>
            {
                new("access_token", accessToken.Token), new("stream", "user")
            };
            var uriBuilder = new UriBuilder(streamingUrl)
            {
                Path = "api/v1/streaming", Query = await new FormUrlEncodedContent(parameters).ReadAsStringAsync()
            };

            _socket = new ClientWebSocket();
            await _socket.ConnectAsync(uriBuilder.Uri, cancellationToken);

            var buffer = new byte[65536];

            try
            {
                while (true)
                {
                    var segment = new ArraySegment<byte>(buffer);

                    var result = await _socket.ReceiveAsync(segment, cancellationToken);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Ok", CancellationToken.None);
                        observer.OnCompleted();
                        return;
                    }

                    var count = result.Count;
                    while (!result.EndOfMessage)
                    {
                        if (count >= buffer.Length)
                        {
                            observer.OnError(new Exception("Payload size is too long"));
                            await _socket.CloseAsync(WebSocketCloseStatus.MessageTooBig, "Payload size is too long",
                                CancellationToken.None);
                            return;
                        }

                        segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                        result = await _socket.ReceiveAsync(segment, cancellationToken);
                        count += result.Count;
                    }

                    var message = Encoding.UTF8.GetString(buffer, 0, count);
                    var (evt, payload) = JsonSerializer.Deserialize<StreamingPayload>(message)!;
                    switch (evt)
                    {
                        case "notification":
                            var notification = JsonSerializer.Deserialize<Notification>(payload)!;
                            observer.OnNext(new UserStreamPayload.Notification(notification));
                            break;
                        case "update":
                            var status = JsonSerializer.Deserialize<Status>(payload)!;
                            observer.OnNext(new UserStreamPayload.Status(status));
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                observer.OnError(e);
            }
        }

        void IDisposable.Dispose()
        {
            _cancellationTokenSource.Cancel();
            _ = _socket?.CloseAsync(WebSocketCloseStatus.NormalClosure, "Ok", CancellationToken.None);
            _socket?.Dispose();
        }

        record StreamingPayload([property: JsonPropertyName("event")] string Event,
            [property: JsonPropertyName("payload")] string Payload);
    }
}
