using System;
using System.Collections.Generic;

namespace ChechenToRussianBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Tutor tutor = new Tutor();
            tutor.AddWord("ж1ал", "собака");
            Console.WriteLine(tutor.CheckWord("ж1ал", "собак"));
            
        }
    }
}
