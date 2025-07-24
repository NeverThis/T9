namespace Infrastructure.Interfaces
{
    public interface IDeserializer
    {
        T Deserialize<T>(string content);
    }
}
