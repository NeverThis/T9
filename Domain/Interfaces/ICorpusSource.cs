namespace Domain.Interfaces
{
    public interface ICorpusSource
    {
        int ReadBlock(char[] buffer, int offset, int count);

        int Read();
    }
}
