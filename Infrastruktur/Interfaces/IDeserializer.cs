namespace Domain.Interfaces
{
    public interface IDeserializer
    {
        T Deserialize<T>(string content);
    }
}
