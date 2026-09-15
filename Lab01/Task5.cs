using System;

namespace oop_course
{
    public static class Task5
    {
        public static void Run()
        {
            Console.Write("Введіть номер дня тижня (1-7): ");
            int day = int.Parse(Console.ReadLine()!);

            switch (day)
            {
                case 1:
                    Console.WriteLine("Понеділок 08:00–18:00");
                    break;
                case 2:
                    Console.WriteLine("Вівторок 08:00–18:00");
                    break;
                case 3:
                    Console.WriteLine("Середа 09:00–17:00");
                    break;
                case 4:
                    Console.WriteLine("Четвер 08:00–18:00");
                    break;
                case 5:
                    Console.WriteLine("П'ятниця 08:00–16:00");
                    break;
                case 6:
                    Console.WriteLine("Субота 09:00–14:00");
                    break;
                case 7:
                    Console.WriteLine("Неділя вихідний");
                    break;
                default:
                    Console.WriteLine("Некоректний день");
                    break;
            }
        }
    }
}