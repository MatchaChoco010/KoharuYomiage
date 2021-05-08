using System;
using MastodonApi;

var hostName = "social.orito-itsuki.net";

var (client_id, client_secret) = await Api.RegisterApp(hostName);

Console.WriteLine(await Api.GetAuthorizeUri(hostName, client_id));
var code = Console.ReadLine()?.Trim() ?? "";

var token = await Api.AuthorizeWithCode(hostName, client_id, client_secret, code);

Console.WriteLine(await Api.GetAccountInformation(hostName, token));
