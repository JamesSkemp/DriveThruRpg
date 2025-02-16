using DriveThruRpgApi.Models;
using System.Net.Http.Json;
using System.Text.Json;

public class ApiClient
{
    private string apiBaseUrl = "https://api.drivethrurpg.com/api/vBeta/";
    private readonly HttpClient httpClient;
    private string applicationKey;
    private ApiTokenResponse? apiTokenResponse;

    public ApiClient(HttpClient httpClient, string applicationKey)
    {
        this.httpClient = httpClient;
        this.applicationKey = applicationKey;
    }

    public bool GetToken()
    {
        var headers = httpClient.DefaultRequestHeaders;
        //headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", applicationKey);

        var request = new HttpRequestMessage(HttpMethod.Post, apiBaseUrl + $"auth_key?applicationKey={applicationKey}")
        {
            Content = JsonContent.Create(new { }),
        };

        var response = httpClient.Send(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"DriveThruRPG API failed to create a token: {(int)response.StatusCode} {response.ReasonPhrase}");
        }

        var result = response.Content.ReadAsStringAsync().Result;

        var tokenResponse = JsonSerializer.Deserialize<ApiTokenResponse>(result);

        if (tokenResponse != null)
        {
            apiTokenResponse = tokenResponse;

            return true;
        }

        return false;
    }

    public ApiProductResponse GetProducts(int page = 1, int perPage = 15)
    {
        // TODO validate access token

        var headers = httpClient.DefaultRequestHeaders;
        headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(GetAccessToken());

        var queryParameters = new Dictionary<string, string> {
            { "page", page.ToString() },
            { "pageSize", perPage.ToString() }
        };

        var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
        var queryString = dictFormUrlEncoded.ReadAsStringAsync().Result;

        var request = new HttpRequestMessage(HttpMethod.Get, apiBaseUrl + $"order_products?{queryString}");

        var response = httpClient.Send(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"DriveThruRPG API failed to get products: {(int)response.StatusCode} {response.ReasonPhrase}");
        }

        //Console.WriteLine(JsonSerializer.Serialize(response));
        //Console.WriteLine(response.Content.ReadAsStringAsync().Result);

        var result = response.Content.ReadAsStringAsync().Result;

        var productResponse = JsonSerializer.Deserialize<ApiProductResponse>(result);

        if (productResponse != null && productResponse.Data != null)
        {
            return productResponse;
        }

        return null;
    }

    private string GetAccessToken()
    {
        return apiTokenResponse?.Token != null ? apiTokenResponse.Token : throw new Exception($"Unable to get a valid access token.");
    }
}
