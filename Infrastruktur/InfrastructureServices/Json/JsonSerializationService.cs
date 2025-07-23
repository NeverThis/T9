using Domain.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.InfrastructureServices.Json
{
    public class JsonSerializationService : ISerializer
    {
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
        };

        public string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, _options);
        }
    }
}
