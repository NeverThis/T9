namespace Domain.Interfaces
{
    public interface ICorpusSource
    {
        int ReadBlock(char[] buffer, int offset, uint count);

        int Read();

        string ReadFully();
    }
}
