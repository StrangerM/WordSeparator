using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    public class FileWorker
    {
        public List<string> GermanWords { get; private set; } = new List<string>();

        private IFileWrapper _fileWrapper;
        public FileWorker(IFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }

        public async Task<List<string>> GetWordsFromDictionary(string path, string dictionary)
        {
            string fullPath = Path.Combine(path, dictionary);
            if (!_fileWrapper.FileExist(fullPath))
            {
                throw new FileNotFoundException();
            }

            var wordList = await _fileWrapper.ReadAllLinesAsync(fullPath);
            GermanWords = wordList.ToList();

            return GermanWords;
        }

        public async Task SaveData(string path, IEnumerable<string> text)
        {
            if (string.IsNullOrEmpty(path) || text is null)
            {
                throw new ArgumentNullException();
            }

           await _fileWrapper.WriteAllLineAsync(path, text);
        }

        public async Task<string[]> GetWordsFromDictionary(string path)
        {
           
            if (!_fileWrapper.FileExist(path))
            {
                throw new FileNotFoundException();
            }
            if (!path.Contains(".txt"))
            {
                throw new Exception();
            }

            var wordList = await _fileWrapper.ReadAllLinesAsync(path);
            return wordList.Where(x=> !string.IsNullOrWhiteSpace(x)).Distinct().ToArray();  
        }
    }
}
