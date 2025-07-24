using Infrastructure.InfrastructureServices.Json;
using Infrastructure.Interfaces;

namespace Infrastructure.Factories
{
    internal class SerializerFactory
    {
        public static ISerializer GetSerializer(string fileType)
        {
            return fileType.ToUpper() switch
            {
                "JSON" => new JsonSerializationService(),
                _ => throw new NotSupportedException($"File type '{fileType}' is not supported.")
            };
        }
    }
}
