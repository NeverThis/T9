namespace Infrastructure.Interfaces
{
    public interface ISerializer
    {
        string Serialize<T>(T obj);
    }
}
