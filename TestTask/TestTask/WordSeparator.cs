using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace TestTask
{
    public class WordSeparator
    {
        public List<string> Results { get; set; } = new List<string>();
        public List<string> DataToWrite { get; set; } = new List<string>();

        public async Task FindAndSeparateWord(List<string> dictionaty, string[] words)
        {
            if (dictionaty is null || words is null)
            {
                throw new ArgumentNullException();
            }

            if (dictionaty.Count == 0 || words.Length == 0)
            {
                throw new ArgumentException();
            }

            await Task.Run(() =>
            {
               foreach (var item in words)
               {
                   Find(item, dictionaty);
                   DataToWrite.Add($"(in){item.ToLower()}->(out){Results.ConvertToString<string>()}");
                   Results.Clear();
               }
            });
        }

        private void Find(string word, List<string> dictionaty)
        {
            try
            {
                List<string> resultTemp = new List<string>();
                char[] letters = word.ToArray();
                letters[0] = Char.ToUpper(letters[0]);
                word = new string(letters);

                var wordsDictionary = dictionaty.Where(x => x.StartsWith(letters[0])).ToList();
                wordsDictionary = wordsDictionary.Where(x => x.Length > 1).ToList();
                for (int x = 1; x <= word.Length; x++)
                {
                    string part = word.Substring(0, x);

                    foreach (var item in wordsDictionary)
                    {
                        if (part == item)
                        {
                            resultTemp.Add(item);
                        }
                    }
                }

                if (resultTemp.Count == 0)
                {
                    if (Results.Count != 0)
                    {
                        word = Results.ToString<string>() + word;
                    }

                    Results.Clear();
                    Results.Add(word);
                    return;
                }

                var mostBigger = resultTemp.Last();

                if (mostBigger != word)
                {
                    Results.Add(mostBigger);
                 
                    string otherPart = word.Substring((mostBigger.Length), (word.Length - 1) - (mostBigger.Length - 1));
                    Find(otherPart, dictionaty);
                }
                else
                {
                    Results.Add(mostBigger);
                    Results = Results.Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }      
    }

    static class ListExtension
    {
        public static string ConvertToString<T>(this List<T> items)
        {
            var result = string.Empty;
            foreach (var item in items)
            {
                result +=item.ToString() + ", ";
            }

            return result.ToLower();
        }

        public static string ToString<T>(this List<T> items)
        {
            var result = string.Empty;
            foreach (var item in items)
            {
                result += item.ToString();
            }

            return result.ToLower(); ;
        }
    }
}
