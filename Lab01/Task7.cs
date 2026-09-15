using System;
using System.Globalization;
using System.Linq;
// Task 7 у Task7.cs
namespace oop_course
{
    public static class Task7
    {
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine()!);
            if (n <= 0) return;

            double[] costs = new double[n];
            for (int i = 0; i < n; i++)
            {
                costs[i] = double.Parse(Console.ReadLine()!.Replace(',', '.'), CultureInfo.InvariantCulture);
            }

            double sum = costs.Sum();
            double avg = costs.Average();
            double min = costs.Min();
            double max = costs.Max();

            Console.WriteLine($"сума: {sum:F2}");
            Console.WriteLine($"середнє: {avg:F2}");
            Console.WriteLine($"мінімум: {min:F2}");
            Console.WriteLine($"максимум: {max:F2}");
        }
    }
}