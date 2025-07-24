using Infrastructure.InfrastructureServices.Json;
using Infrastructure.Interfaces;

namespace Infrastructure.Factories
{
    internal class DeserializerFactory
    {
        public static IDeserializer GetDeserializer(string fileType)
        {
            return fileType.ToUpper() switch
            {
                "JSON" => new JsonDeserializationService(),
                _ => throw new NotSupportedException($"File type '{fileType}' is not supported.")
            };
        }
    }
}
