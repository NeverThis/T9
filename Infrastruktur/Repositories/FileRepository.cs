using Domain.Interfaces;
using Domain.ValueObjects.Trees;
using Infrastructure.FileAccess;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class FileRepository(ISerializer serializer, IDeserializer deserializer, string path) : IModelRepository
    {
        private readonly ISerializer _serializer = serializer;
        private readonly IDeserializer _deserializer = deserializer;
        private readonly string _path = path;

        public NGram Load()
        {
            using var reader = new FileReader(_path);
            var content = reader.ReadFully();
            return _deserializer.Deserialize<NGram>(content);
        }

        public void Save(NGram toSave)
        {
            var serializedData = _serializer.Serialize(toSave);
            FileWriter.Store(_path, serializedData);
        }
    }
}
