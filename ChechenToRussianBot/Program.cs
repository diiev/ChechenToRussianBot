using System;
using System.Collections.Generic;
using Telegram.Bot;

namespace ChechenToRussianBot
{
    class Program
    {   
        static Tutor tutor = new Tutor();
        static TelegramBotClient Bot;
        static Dictionary<int, string> LastWord = new Dictionary<int, string>();
        const string COMMAND_LIST =
            @"Cписок команд:
            /add -   <chechen> <rus> - добавление чеченского слова и его перевод в словарь
            /get -   получаем случайное чеченское слово из словаря
            /check - <chechen> <rus> - проверяем правильность перевода чеченского слова
            ";
            
        static void Main(string[] args)
        {

           
            Bot = new TelegramBotClient("1537627062:AAFwFrmGNvirsfWi2JFzRGKC7jLeYKg7auU");

            Bot.OnMessage += Bot_OnMessage;
            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }

        static string AddWords (String [] msgArr)
        {
            if (msgArr.Length != 3)
                return "Неправильное количество аргументов. Их должно быть 2";
            else
            {
                tutor.AddWord(msgArr[1], msgArr[2]);
                return "Новое слово добавлено словарь";
            }
        }
        private async static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {

            if (e == null || e.Message == null || e.Message.Type != Telegram.Bot.Types.Enums.MessageType.Text)
            {
                return;
            }
            var userId = e.Message.From.Id;
            var msgArgs = e.Message.Text.Split(' ');
            String text;
          
            switch (msgArgs[0])
            {
                case "/start":
                    text = COMMAND_LIST;
                    break;
                case "/add": text = AddWords(msgArgs);
                    break;
                case "/get":
                    text = GetRandomChechWord(userId);
                    break;
                case "/check": text = CheckWord(msgArgs);
                    var newWord = GetRandomChechWord(userId);
                    text = $"{text}\r\nСледующее слово: {newWord}";
                    break;
                default: 
                    if (LastWord.ContainsKey(userId))
                    {
                        text = CheckWord(LastWord[userId], msgArgs[0]);
                        newWord = GetRandomChechWord(userId);
                        text = $"{text}\r\nСледующее слово: {newWord}";
                    } 
                    else
                    {
                        text = COMMAND_LIST;
                    }
                    
                    break;
                  
              
            } 

            await Bot.SendTextMessageAsync(e.Message.From.Id, text);

        }

        private static string GetRandomChechWord(int userId)
        {
            var text = tutor.GetRandomChechWord();
            if (LastWord.ContainsKey(userId))
                LastWord[userId] = text;
            else
                LastWord.Add(userId, text);
            return text;
        }

        private static string CheckWord(string[] msgArgs)
        {
            if (msgArgs.Length != 3)
                return "Неправильное количество аргументов. Их должно быть 2";
            else
                return CheckWord(msgArgs[1], msgArgs[2]);
        } 
        private static string CheckWord(string chechen, string rus)
        {
            if (tutor.CheckWord(chechen, rus))
                return "Правильно";
            else
            {
                var correctAnswer = tutor.Translate(chechen);
                return $"Неверно, Правильный ответ: {correctAnswer}";
            }
        }
    }
}
