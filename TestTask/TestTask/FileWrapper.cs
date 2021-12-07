using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    public class FileWrapper : IFileWrapper
    {
        public void FileCopy(string filePath, string destination)
        {
            File.Copy(filePath, destination);
        }

        public void FileCreate(string path)
        {
            File.Create(path);
        }

        public bool FileExist(string path)
        {
            return File.Exists(path);
        }

        public async Task<string[]> ReadAllLinesAsync(string path)
        {
            return await File.ReadAllLinesAsync(path);
        }

        public async Task<string> ReadAllTextAsync(string path)
        {
            return await File.ReadAllTextAsync(path);
        }

        public async Task WriteAllLineAsync(string path, IEnumerable<string> text)
        {
            await File.WriteAllLinesAsync(path, text);
        }

        public async Task WriteAllTextAsync(string path, string textToWrite)
        {
             await File.WriteAllTextAsync(path, textToWrite);
        }
    }
}
