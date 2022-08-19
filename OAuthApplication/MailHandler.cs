using System;
using System.Collections;
using System.Collections.Generic;
using MimeKit;
using MailKit;
using MailKit.Search;
using MailKit.Security;
using MailKit.Net.Imap;

namespace OAuthApplication;

public class MailHandler
{
    private static TaskCompletionSource<bool> tcs = new(false);

    private const string mail = "mail@mail.com";

    private const string host = "outlook.office365.com";
    private const int port = 993;

    public static async Task GetAllMessagesAsync(string auth_token)
	{
        using var client = new ImapClient();

        await client.ConnectAsync(host, port, SecureSocketOptions.Auto);

        Console.WriteLine($"Negotiated the following SSL options with {host}:");
        Console.WriteLine($"        Protocol Version: {client.SslProtocol}");
        Console.WriteLine($"        Cipher Algorithm: {client.SslCipherAlgorithm}");
        Console.WriteLine($"         Cipher Strength: {client.SslCipherStrength}");
        Console.WriteLine($"          Hash Algorithm: {client.SslHashAlgorithm}");
        Console.WriteLine($"           Hash Strength: {client.SslHashStrength}");
        Console.WriteLine($"  Key-Exchange Algorithm: {client.SslKeyExchangeAlgorithm}");
        Console.WriteLine($"   Key-Exchange Strength: {client.SslKeyExchangeStrength}");

        var oauth2 = new SaslMechanismOAuth2(mail, auth_token);

        client.Authenticate(oauth2);


        client.Inbox.Open(FolderAccess.ReadOnly);
        int uid = client.Inbox.FirstUnread;


        Console.WriteLine($"FIRST UNREAD MAIL={client.Inbox.GetMessage(uid).Subject}");


        client.Disconnect(true);
    }



}
