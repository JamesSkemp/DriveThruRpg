using System.Text.Json;
using System.Text.Json.Serialization;

namespace DriveThruRpgApi.Models
{
    public class ApiJsonDateTimeConvertor : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!string.IsNullOrWhiteSpace(reader.GetString())) {
                return DateTime.Parse(reader.GetString());
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
