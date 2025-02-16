using System.Text.Json.Serialization;

namespace DriveThruRpgApi.Models
{
    public class ApiTokenResponse
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }
        [JsonPropertyName("refreshToken")]
        public string? RefreshToken { get; set; }
        [JsonPropertyName("refreshTokenTTL")]
        public int RefreshTokenTtl { get; set; }
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
