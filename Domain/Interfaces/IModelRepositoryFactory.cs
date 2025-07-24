namespace Domain.Interfaces
{
    public interface IModelRepositoryFactory
    {
        IModelRepository Create(string fileType, string filePath);
    }
}
