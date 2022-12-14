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

    public ApiProductResponse GetProducts(int page = 1, int perPage = 15) {
        // TODO validate access token

        var headers = httpClient.DefaultRequestHeaders;
        headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GetAccessToken());

        var queryParameters = new Dictionary<string, string> {
            { "page", page.ToString() },
            { "per_page", perPage.ToString() }
        };

        var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
        var queryString = dictFormUrlEncoded.ReadAsStringAsync().Result;

        var request = new HttpRequestMessage(HttpMethod.Get, apiBaseUrl + $"customers/{apiTokenResponse.CustomersId}/products?{queryString}");

        var response = httpClient.Send(request);

        if (!response.IsSuccessStatusCode) {
            throw new Exception($"DriveThruRPG API failed to get products: {(int)response.StatusCode} {response.ReasonPhrase}");
        }

        //Console.WriteLine(JsonSerializer.Serialize(response));
        //Console.WriteLine(response.Content.ReadAsStringAsync().Result);

        var productResponse = JsonSerializer.Deserialize<ApiProductResponse>(response.Content.ReadAsStringAsync().Result);

        if (productResponse != null && productResponse.Message != null) {
            return productResponse;
        }

        return null;
    }

    public string GetCustomerId() {
        if (apiTokenResponse == null) {
            return "";
        }
        return apiTokenResponse.CustomersId;
    }

    private string GetAccessToken() {
        return apiTokenResponse?.AccessToken;
    }
}
