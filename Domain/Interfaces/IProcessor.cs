namespace Domain.Interfaces
{
    internal interface IProcessor
    {
        void Process(ReadOnlySpan<char> window);
    }
}
