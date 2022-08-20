using MailKit.Security;
using MailKit.Net.Imap;
using MailKit;
using MimeKit;

namespace OAuthApplication;

public class MailHandler
{
    //The mail you want to read messages from
    private const string mail = Secrets.mail;

    private const string host = "outlook.office365.com";
    private const int port = 993;

    public static async Task<List<MimeMessage>> GetAllMessagesAsync(AccessTokenModel accessToken)
	{
        using var client = new ImapClient();

        await IMAPConnectAsync(client, accessToken);

        var messages = new List<MimeMessage>();
        var uids = await client.Inbox.SearchAsync(MailKit.Search.SearchQuery.All);
        foreach (var uid in uids)
        {
            messages.Add(await client.Inbox.GetMessageAsync(uid));
        }

        await client.DisconnectAsync(true);
        return messages;
    }

    public static async Task<MimeMessage> GetFirstUnreadMessageAsync(AccessTokenModel accessToken)
    {
        using var client = new ImapClient();

        await IMAPConnectAsync(client, accessToken);

        int unread_index = client.Inbox.FirstUnread;
        return await client.Inbox.GetMessageAsync(unread_index);

    }

    private static async Task IMAPConnectAsync(ImapClient client, AccessTokenModel accessToken, FolderAccess folderAccess = FolderAccess.ReadOnly)
    {
        var oauth2 = new SaslMechanismOAuth2(mail, accessToken.access_token);

        await client.ConnectAsync(host, port, SecureSocketOptions.Auto);
        await client.AuthenticateAsync(oauth2);

        await client.Inbox.OpenAsync(folderAccess);
    }


}
