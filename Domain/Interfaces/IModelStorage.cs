namespace Domain.Interfaces
{
    public interface IModelStorage<T>
    {
        void Store(T location, string content);
    }
}
