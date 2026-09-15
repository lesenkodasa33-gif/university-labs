using System;
using System.Globalization;

namespace oop_course
{
    public static class Task7
    {
        public static void Run()
        {
            int n = int.Parse(Console.ReadLine()!);
            decimal[] costs = new decimal[n];

            for (int i = 0; i < n; i++)
            {
                costs[i] = decimal.Parse(Console.ReadLine()!.Replace(',', '.'), CultureInfo.InvariantCulture);
            }

            // 1. foreach: накопичення sum, пошук min та max
            decimal sum = 0m;
            decimal min = costs[0];
            decimal max = costs[0];

            foreach (decimal c in costs)
            {
                sum += c;
                if (c < min) min = c;
                if (c > max) max = c;
            }

            decimal avg = sum / n;

            // 2. for: підрахунок елементів strictly > avg
            int countAboveAvg = 0;
            for (int i = 0; i < n; i++)
            {
                if (costs[i] > avg)
                {
                    countAboveAvg++;
                }
            }

            // 3. while: пошук першого елемента > 1000
            int idx = 0;
            bool found = false;
            while (idx < n && !found)
            {
                if (costs[idx] > 1000m)
                {
                    found = true;
                }
                else
                {
                    idx++;
                }
            }

            // Вивід звітних даних
            Console.WriteLine("=== Звіт по прийомах ===");
            Console.WriteLine($"Кількість прийомів: {n}");
            Console.WriteLine($"Загальна сума: {sum:F2} грн");
            Console.WriteLine($"Середня сума: {avg:F2} грн");
            Console.WriteLine($"Мінімальна сума: {min:F2} грн");
            Console.WriteLine($"Максимальна сума: {max:F2} грн");
            Console.WriteLine($"Дорожчих за середню: {countAboveAvg} з {n}");

            if (found)
            {
                Console.WriteLine($"Перший прийом > 1000 грн: №{idx + 1} ({costs[idx]:F2} грн)");
            }
            else
            {
                Console.WriteLine("Перший прийом > 1000 грн: немає");
            }
        }
    }
}