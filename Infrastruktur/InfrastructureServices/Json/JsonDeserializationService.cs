using Infrastructure.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.InfrastructureServices.Json
{
    public class JsonDeserializationService : IDeserializer
    {
        private readonly JsonSerializerOptions _options = new()
        {
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        };

        public T Deserialize<T>(string content)
        {
            return JsonSerializer.Deserialize<T>(content, _options)
                   ?? throw new InvalidOperationException("Deserialization returned null.");
        }
    }
}
