using OAuthApplication;

ApiHandler.InitializeClient();

var accessToken = await ApiHandler.GetAccessTokenAsync();

if (!string.IsNullOrEmpty(accessToken.access_token))
{
    Console.WriteLine("Obtained access token");
    await MailHandler.GetAllMessagesAsync(accessToken.access_token);
}
