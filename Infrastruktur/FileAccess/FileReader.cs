using Domain.Interfaces;
using System.IO;

namespace Infrastructure.FileAccess
{
    public class FileReader : IDisposable, ICorpusSource
    {
        private readonly StreamReader _reader;

        public FileReader(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path cannot be null or whitespace!", nameof(filePath));

            try
            {
                _reader = new StreamReader(filePath);
            }
            catch (Exception ex)
            {
                throw new IOException($"Failed to open the file '{filePath}'!", ex);
            }
        }

        public int ReadBlock(char[] buffer, int offset, uint count)
        {
            return _reader.ReadBlock(buffer, offset, Convert.ToInt32(count));
        }

        public int Read()
        {
            return _reader.Read();
        }

        public string ReadFully()
        {
            try
            {
                return _reader.ReadToEnd();
            }
            catch (FileNotFoundException ex)
            {
                Console.Error.WriteLine($"File not found: {ex.Message}");
                throw;
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine($"IO error reading: {ex.Message}");
                throw;
            }
        }

        public void Dispose() => _reader.Dispose();
    }
}
