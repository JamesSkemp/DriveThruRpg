using System.Net.Http.Json;
using System.Text.Json;
using DriveThruRpgApi.Models;

public class ApiClient
{
    private string apiBaseUrl = "https://www.drivethrurpg.com/api/v1/";
    private readonly HttpClient httpClient;
    private string applicationKey;
    private ApiTokenMessageResponse? apiTokenResponse;

    public ApiClient(HttpClient httpClient, string applicationKey)
    {
        this.httpClient = httpClient;
        this.applicationKey = applicationKey;
    }

    public bool GetToken()
    {
        var headers = httpClient.DefaultRequestHeaders;
        headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", applicationKey);

        var request = new HttpRequestMessage(HttpMethod.Post, apiBaseUrl + "token")
        {
            Content = JsonContent.Create(new { })
        };

        var response = httpClient.Send(request);

        if (!response.IsSuccessStatusCode) {
            throw new Exception($"DriveThruRPG API failed to create a token: {(int)response.StatusCode} {response.ReasonPhrase}");
        }

        var tokenResponse = JsonSerializer.Deserialize<ApiTokenResponse>(response.Content.ReadAsStringAsync().Result);

        if (tokenResponse != null && tokenResponse.Message != null) {
            apiTokenResponse = tokenResponse.Message;

            return true;
        }

        return false;
    }
}
