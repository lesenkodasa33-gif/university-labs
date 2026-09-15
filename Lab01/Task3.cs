using System;
// Task 3 у Task3.cs
namespace oop_course
{
    public static class Task3
    {
        public static void Run()
        {
            Console.Write("Введіть рік народження: ");
            int birthYear = int.Parse(Console.ReadLine()!);

            int age = 2026 - birthYear;

            string category;
            if (age <= 17)
            {
                category = "дитина";
            }
            else if (age <= 59)
            {
                category = "дорослий";
            }
            else
            {
                category = "пенсіонер";
            }

            Console.WriteLine(category);
        }
    }
}