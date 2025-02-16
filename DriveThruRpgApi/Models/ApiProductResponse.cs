using System.Text.Json.Serialization;

namespace DriveThruRpgApi.Models
{
    public class ApiProductResponse
    {
        [JsonPropertyName("data")]
        public List<ApiProductDataResponse>? Data { get; set; }
    }

    public class ApiProductDataResponse
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("attributes")]
        public ApiProductMessageResponse Products { get; set; }
    }

    public class ApiProductMessageResponse
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
        [JsonPropertyName("productId")]
        public int ProductsId { get; set; }
        [JsonPropertyName("name")]
        public string? ProductsName { get; set; }
        [JsonPropertyName("archived")]
        public int IsArchived { get; set; }
        /*[JsonPropertyName("cover_url")]
        public string? CoverUrl { get; set; }*/
        [JsonPropertyName("datePurchased")]
        public string? DatePurchased { get; set; }
    }
}
