namespace Domain.Interfaces
{
    public interface ISerializer
    {
        string Serialize<T>(T obj);
    }
}
