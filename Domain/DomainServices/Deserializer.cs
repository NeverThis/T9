using Domain.Interfaces;

namespace Domain.DomainServices
{
    public class Deserializer
    {
        private readonly IDeserializer _deserializer;
        private readonly ICorpusSource _source;

        public Deserializer(IDeserializer deserializer, ICorpusSource source)
        {
            _deserializer = deserializer;
            _source = source;
        }

        public T Deserialize<T>()
        {
            var content = _source.ReadFully();
            return _deserializer.Deserialize<T>(content);
        }
    }
}
