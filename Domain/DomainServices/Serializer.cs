

using Domain.Interfaces;

namespace Domain.DomainServices
{
    public class Serializer<T>
    {
        private readonly ISerializer _serializer;
        private readonly IModelStorage<T> _storer;

        public Serializer(ISerializer serializer, IModelStorage<T> storer)
        {
            _serializer = serializer;
            _storer = storer;
        }

        public void Serialize<O>(O toSerialize, T path)
        {
            var json = _serializer.Serialize(toSerialize);
            _storer.Store(path, json);
        }
    }
}
