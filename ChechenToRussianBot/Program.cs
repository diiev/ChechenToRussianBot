using System;
using System.Collections.Generic;

namespace ChechenToRussianBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var tutor = new Tutor();
            tutor.AddWord("ж1аьл", "собака");
            tutor.AddWord("цициг", "кошка");
            tutor.AddWord("д1аг1о", "уходи");
            tutor.AddWord("дахк", "мышь");

            while (true)
            {
                var word = tutor.GetRandomChechWord();
                Console.WriteLine($"Как переводится слово: {word} ");
                var userAnswer = Console.ReadLine();
                if (tutor.CheckWord(word,userAnswer))
                    Console.WriteLine("Правильно");
                else
                {
                    var correctAnswer = tutor.Translate(word);
                    Console.WriteLine($"Неверно. Правильный ответ: {correctAnswer}");

                }
            }
        }
    }
}
