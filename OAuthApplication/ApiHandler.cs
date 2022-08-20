using System.Net.Http.Headers;

namespace OAuthApplication;

public class ApiHandler
{
    public static HttpClient ApiClient { get; set; } = new HttpClient();

    //These values can be found in your azure ad graph application
    private static readonly string tenant_id = Secrets.tenant_id;
    private static readonly string client_id = Secrets.client_id;

    private static readonly string client_secret = Secrets.client_secret;

    public static void InitializeClient()
    {
        if (ApiClient == null)
            ApiClient = new HttpClient();

        ApiClient.DefaultRequestHeaders.Accept.Clear();
        ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static async Task<AccessTokenModel> GetAccessTokenAsync()
    {
        string url = $"https://login.microsoftonline.com/{tenant_id}/oauth2/v2.0/token";

        var data = new Dictionary<string, string>
        {
            {"grant_type", "client_credentials"},
            {"scope", "https://outlook.office365.com/.default"},
            {"client_id",  client_id},
            {"client_secret", client_secret}
        };

        using HttpClient client = new();
        var response = await client.PostAsync(url, new FormUrlEncodedContent(data));
        return await response.Content.ReadAsAsync<AccessTokenModel>();
    }

}

