using System.Text.Json.Serialization;

namespace DriveThruRpgApi.Models
{
    public class ApiProductResponse
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("message")]
        public List<ApiProductMessageResponse>? Message { get; set; }
    }

    public class ApiProductMessageResponse
    {
        [JsonPropertyName("products_id")]
        public string? ProductsId { get; set; }
        [JsonPropertyName("products_name")]
        public string? ProductsName { get; set; }
        [JsonPropertyName("is_archived")]
        public string? IsArchived { get; set; }
        [JsonPropertyName("cover_url")]
        public string? CoverUrl { get; set; }
        [JsonPropertyName("date_purchased")]
        public string? DatePurchased { get; set; }
    }
}
