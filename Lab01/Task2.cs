using System;
// Task 2 у Task2.cs
namespace oop_course
{
    public static class Task2
    {
        public static void Run()
        {
            Console.Write("Введіть ціну прийому: ");
            double price = double.Parse(Console.ReadLine()!);

            Console.Write("Введіть кількість прийомів: ");
            double count = double.Parse(Console.ReadLine()!);

            Console.Write("Введіть знижку (%): ");
            double discount = double.Parse(Console.ReadLine()!);

            double total = price * count * (1 - discount / 100);

            Console.WriteLine(total);
        }
    }
}