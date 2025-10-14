using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SmartNeighborhoodAPI.Helpers
{
    public class JsonNumberEnumConverter : JsonConverter<HttpStatusCode>
    {
        public override HttpStatusCode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return (HttpStatusCode)reader.GetInt32();
        }

        public override void Write(Utf8JsonWriter writer, HttpStatusCode value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue((int)value);
        }
    }
}
