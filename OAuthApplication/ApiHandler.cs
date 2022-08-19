using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace OAuthApplication;

public class ApiHandler
{
    public static HttpClient ApiClient { get; set; } = new HttpClient();

    private static string client_id = "CLIENT_ID";
    private static string client_secret = "CLIENT_SECRET";
    private static string tenant_id = "TENANT_ID";

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

