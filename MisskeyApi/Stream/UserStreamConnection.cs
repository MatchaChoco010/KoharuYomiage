using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MisskeyApi.Payloads;
using MisskeyApi.Payloads.Entities;

namespace MisskeyApi.Stream
{
    internal class UserStreamConnection : IDisposable
    {
        readonly CancellationTokenSource _cancellationTokenSource = new();
        ClientWebSocket? _socket;

        void IDisposable.Dispose()
        {
            _cancellationTokenSource.Cancel(true);
            _ = _socket?.CloseAsync(WebSocketCloseStatus.NormalClosure, "Ok", CancellationToken.None);
            _socket?.Dispose();
        }

        public async ValueTask Start(IObserver<UserStreamPayload> observer, string hostName, AccessToken accessToken)
        {
            var cancellationToken = _cancellationTokenSource.Token;

            var parameters = new List<KeyValuePair<string, string>>
            {
                new("i", accessToken.Value),
            };
            var uriBuilder = new UriBuilder
            {
                Scheme = "wss",
                Host = hostName,
                Path = "streaming",
                Query = await new FormUrlEncodedContent(parameters).ReadAsStringAsync()
            };

            try
            {
                _socket = new ClientWebSocket();
                await _socket.ConnectAsync(uriBuilder.Uri, cancellationToken);

                var buffer = new byte[65536];

                await _socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new
                    {
                        type = "connect",
                        body = new
                        {
                            channel = "main",
                            id = Guid.NewGuid().ToString(),
                        },
                    }))),
                    WebSocketMessageType.Text,
                    true,
                    cancellationToken);
                await _socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new
                    {
                        type = "connect",
                        body = new
                        {
                            channel = "homeTimeline",
                            id = Guid.NewGuid().ToString(),
                        },
                    }))),
                    WebSocketMessageType.Text,
                    true,
                    cancellationToken);

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
                    var (_, (_, type)) = JsonSerializer.Deserialize<StreamingPayloadType>(message)!;
                    switch (type)
                    {
                        case "notification":
                            var (_, (_, notificationType)) = JsonSerializer.Deserialize<StreamingPayloadNotificationType>(message)!;
                            switch (notificationType.type)
                            {
                                case "reaction":
                                    var (_, (_, reaction)) = JsonSerializer.Deserialize<StreamingPayloadNotificationReaction>(message)!;
                                    observer.OnNext(new UserStreamPayload.Notification(reaction));
                                    break;
                                case "reply":
                                    var (_, (_, reply)) = JsonSerializer.Deserialize<StreamingPayloadNotificationReply>(message)!;
                                    observer.OnNext(new UserStreamPayload.Notification(reply));
                                    break;
                                case "renote":
                                    var (_, (_, renote)) = JsonSerializer.Deserialize<StreamingPayloadNotificationRenote>(message)!;
                                    observer.OnNext(new UserStreamPayload.Notification(renote));
                                    break;
                                case "quote":
                                    var (_, (_, quote)) = JsonSerializer.Deserialize<StreamingPayloadNotificationQuote>(message)!;
                                    observer.OnNext(new UserStreamPayload.Notification(quote));
                                    break;
                                case "mention":
                                    var (_, (_, mention)) = JsonSerializer.Deserialize<StreamingPayloadNotificationMention>(message)!;
                                    observer.OnNext(new UserStreamPayload.Notification(mention));
                                    break;
                                case "pollVote":
                                    var (_, (_, pollVote)) = JsonSerializer.Deserialize<StreamingPayloadNotificationPollVote>(message)!;
                                    observer.OnNext(new UserStreamPayload.Notification(pollVote));
                                    break;
                                case "follow":
                                    var (_, (_, follow)) = JsonSerializer.Deserialize<StreamingPayloadNotificationFollow>(message)!;
                                    observer.OnNext(new UserStreamPayload.Notification(follow));
                                    break;
                                case "followRequestAccepted":
                                    var (_, (_, followRequestAccepted)) = JsonSerializer.Deserialize<StreamingPayloadNotificationFollowRequestAccepted>(message)!;
                                    observer.OnNext(new UserStreamPayload.Notification(followRequestAccepted));
                                    break;
                                case "receiveFollowRequest":
                                    var (_, (_, receiveFollowRequest)) = JsonSerializer.Deserialize<StreamingPayloadNotificationReceiveFollowRequest>(message)!;
                                    observer.OnNext(new UserStreamPayload.Notification(receiveFollowRequest));
                                    break;
                                case "groupInvited":
                                    var (_, (_, groupInvited)) = JsonSerializer.Deserialize<StreamingPayloadNotificationGroupInvited>(message)!;
                                    observer.OnNext(new UserStreamPayload.Notification(groupInvited));
                                    break;
                                case "app":
                                    var (_, (_, app)) = JsonSerializer.Deserialize<StreamingPayloadNotificationApp>(message)!;
                                    observer.OnNext(new UserStreamPayload.Notification(app));
                                    break;
                            }
                            break;
                        case "note":
                            var (_, (_, note)) = JsonSerializer.Deserialize<StreamingPayloadNote>(message)!;
                            observer.OnNext(new UserStreamPayload.Note(note));
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                observer.OnError(e);
            }
        }

        record StreamingPayloadType(string type, StreamingPayloadTypeBody body);
        record StreamingPayloadTypeBody(string id, string type);

        record StreamingPayloadNotificationType(string type, StreamingPayloadNotificationTypeBody body);
        record StreamingPayloadNotificationTypeBody(string type, StreamingPayloadNotificationTypeBodyBody body);

        record StreamingPayloadNotificationTypeBodyBody(string type);

        record StreamingPayloadNotificationReaction(string type, StreamingPayloadNotificationReactionBody body);
        record StreamingPayloadNotificationReactionBody(string type, Notification.Reaction body);
        record StreamingPayloadNotificationReply(string type, StreamingPayloadNotificationReplyBody body);
        record StreamingPayloadNotificationReplyBody(string type, Notification.Reply body);
        record StreamingPayloadNotificationRenote(string type, StreamingPayloadNotificationRenoteBody body);
        record StreamingPayloadNotificationRenoteBody(string type, Notification.Renote body);
        record StreamingPayloadNotificationQuote(string type, StreamingPayloadNotificationQuoteBody body);
        record StreamingPayloadNotificationQuoteBody(string type, Notification.Quote body);
        record StreamingPayloadNotificationMention(string type, StreamingPayloadNotificationMentionBody body);
        record StreamingPayloadNotificationMentionBody(string type, Notification.Mention body);
        record StreamingPayloadNotificationPollVote(string type, StreamingPayloadNotificationPollVoteBody body);
        record StreamingPayloadNotificationPollVoteBody(string type, Notification.PollVote body);
        record StreamingPayloadNotificationFollow(string type, StreamingPayloadNotificationFollowBody body);
        record StreamingPayloadNotificationFollowBody(string type, Notification.Follow body);
        record StreamingPayloadNotificationFollowRequestAccepted(string type, StreamingPayloadNotificationFollowRequestAcceptedBody body);
        record StreamingPayloadNotificationFollowRequestAcceptedBody(string type, Notification.FollowRequestAccepted body);
        record StreamingPayloadNotificationReceiveFollowRequest(string type, StreamingPayloadNotificationReceiveFollowRequestBody body);
        record StreamingPayloadNotificationReceiveFollowRequestBody(string type, Notification.ReceiveFollowRequest body);
        record StreamingPayloadNotificationGroupInvited(string type, StreamingPayloadNotificationGroupInvitedBody body);
        record StreamingPayloadNotificationGroupInvitedBody(string type, Notification.GroupInvited body);
        record StreamingPayloadNotificationApp(string type, StreamingPayloadNotificationAppBody body);
        record StreamingPayloadNotificationAppBody(string type, Notification.App body);

        record StreamingPayloadNote(string type, StreamingPayloadNoteBody body);
        record StreamingPayloadNoteBody(string type, Note body);
    }
}
