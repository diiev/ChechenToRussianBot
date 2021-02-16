using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ChechenToRussianBot
{
    class WordStorage
    {
        private const string _path = "wordstorage.txt";
        public Dictionary<string, string> GetAllWords ()
        {

            try
            {
                var dic = new Dictionary<string, string>();
                if (File.Exists(_path))
                {
                    foreach (var line in File.ReadAllLines(_path))
                    {
                        var words = line.Split('|');
                        if (words.Length == 2)
                        {
                            dic.Add(words[0], words[1]);
                        }
                    }
                }
                return dic;
            }
           
            catch (Exception)
            {
                Console.WriteLine("Не удалость прочитать файл");
                return new Dictionary<string, string>();
            }
        }
        public void AddWord(string chechen, string rus)
        {
            try
            {
                using (var writer = new StreamWriter(_path, true))
                {
                    writer.WriteLine($"{chechen}|{rus}");
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Не удалось добавить слово");
            }
           
        }
    }
}
