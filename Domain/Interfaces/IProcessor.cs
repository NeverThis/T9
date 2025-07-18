namespace Domain.Interfaces
{
    public interface IProcessor
    {
        void Process(ReadOnlySpan<char> window);
    }
}
