using OAuthApplication;


ApiHandler.InitializeClient();

var accessToken = await ApiHandler.GetAccessTokenAsync();

if (!string.IsNullOrEmpty(accessToken.access_token))
{
    var messages = await MailHandler.GetAllMessagesAsync(accessToken);
    messages.ForEach(message => Console.WriteLine(message.Subject));
}
