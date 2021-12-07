using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    public interface IFileWrapper
    {
        Task<string[]> ReadAllLinesAsync(string path);
        Task<string> ReadAllTextAsync(string path);
        Task WriteAllTextAsync(string path, string textToWrite);
        void FileCopy(string filePath, string destination);
        bool FileExist(string path);
        void FileCreate(string path);
        Task WriteAllLineAsync(string path, IEnumerable<string> text);
    }
}
