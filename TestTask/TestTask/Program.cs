using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTask
{
    class Program
    {
        const string dictionaryName = "de-dictionary.tsv";
        static async Task Main(string[] args)
        {
            FileWorker worker = new FileWorker(new FileWrapper());
            WordSeparator wordSeparator = new WordSeparator();

            Console.WriteLine("Please type the path to file(format .txt) with words. Every new word(in file) should be in a new line.");

            Console.WriteLine(@"Example - C:\Users\Desktop\dictionaty.txt, and Press Enter");

            string path = Console.ReadLine();
           
            await worker.GetWordsFromDictionary(AppDomain.CurrentDomain.BaseDirectory, dictionaryName);

            var dictionaryForCheck = await worker.GetWordsFromDictionary(@path);

            await wordSeparator.FindAndSeparateWord(worker.GermanWords, dictionaryForCheck);

            Console.WriteLine(@"Please type the path to file(format.txt) where result will be saved Example- C:\Users\Desktop\11.txt");
            Console.WriteLine("If File not exists, file will be created. Press Enter ");

            string pathForRes = Console.ReadLine();
            await worker.SaveData(@pathForRes, wordSeparator.DataToWrite);

            Console.WriteLine("File saved successfully ");
        }
    }
}
