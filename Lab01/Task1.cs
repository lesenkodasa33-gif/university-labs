using System;

namespace oop_course
{
    public static class Task1
    {
        public static void Run()
        {
            Console.Write("Введіть вагу пацієнта (кг): ");
            string vstr = Console.ReadLine()!;
            double w = double.Parse(vstr);

            Console.Write("Введіть зріст пацієнта (м): ");
            string zstr = Console.ReadLine()!;
            double h = double.Parse(zstr);

            double imt = w / (h * h);
            Console.WriteLine($"IMT: {imt:F2}");
        }
    }
}