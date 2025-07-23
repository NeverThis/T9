using System.IO;

namespace Infrastruktur.FileAccess
{
    public class FileWriter
    {
        public static void Store(string location, string content)
        {
            try
            {
                File.WriteAllText(location, content);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.Error.WriteLine($"Access denied to file '{location}': {ex.Message}");
                throw;
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine($"IO error while writing to '{location}': {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error writing to '{location}': {ex.Message}");
                throw;
            }
        }
    }
}
