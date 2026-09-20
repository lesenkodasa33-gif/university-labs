using System;
using System.Globalization;
using System.Linq;

namespace oop_course.Lab02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
                return;

            double[] weights = new double[n];
            int count = 0;

            while (count < n)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    if (count < n && double.TryParse(part, NumberStyles.Any, CultureInfo.InvariantCulture, out double weight))
                    {
                        weights[count] = weight;
                        count++;
                    }
                }
            }

            double min = weights.Min();
            double max = weights.Max();
            double average = weights.Average();
            int aboveAverageCount = weights.Count(w => w > average);

          
            Console.WriteLine(average);
            Console.WriteLine(min);
            Console.WriteLine(max);
            Console.WriteLine(aboveAverageCount);
        }
    }
}