using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure.Factories
{
    public class ModelRepositoryFactory : IModelRepositoryFactory
    {
        public IModelRepository Create(string fileType, string filePath)
        {
            var serializer = SerializerFactory.GetSerializer(fileType);
            var deserializer = DeserializerFactory.GetDeserializer(fileType);
            return new FileRepository(serializer, deserializer, filePath);
        }
    }
}
