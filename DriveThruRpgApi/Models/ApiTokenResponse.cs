using System.Text.Json.Serialization;

namespace DriveThruRpgApi.Models
{
    public class ApiTokenResponse
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("message")]
        public ApiTokenMessageResponse? Message { get; set; }
    }

    public class ApiTokenMessageResponse
    {
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }
        [JsonPropertyName("customers_id")]
        public string? CustomersId { get; set; }
        [JsonPropertyName("expires"), JsonConverter(typeof(ApiJsonDateTimeConvertor))]
        public DateTime? Expires { get; set; }
        [JsonPropertyName("jwt")]
        public string? Jwt { get; set; }
        [JsonPropertyName("server_time"), JsonConverter(typeof(ApiJsonDateTimeConvertor))]
        public DateTime? ServerTime { get; set; }
    }
}
