using Domain.Interfaces;
using System;
using System.IO;
using System.Windows.Shapes;

namespace Infrastruktur.FileAccess
{
    public class FileWriter : IModelStorage<string>
    {
        public void Store(string location, string content)
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
