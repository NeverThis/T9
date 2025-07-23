using Domain.ValueObjects.Trees;

namespace Domain.Interfaces
{
    public interface IModelRepository
    {
        NGram Load();
        void Save(NGram toSave);
    }
}
