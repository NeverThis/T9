using Domain.Interfaces;
using System.IO;

namespace Infrastruktur.FileAccess
{
    internal class FileReader(string filePath) : IDisposable, ICorpusSource
    {
        private readonly StreamReader _reader = new(filePath);

        public int ReadBlock(char[] buffer, int offset, int count)
        {
            return _reader.ReadBlock(buffer, offset, count);
        }

        public int Read()
        {
            return _reader.Read();
        }

        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}
