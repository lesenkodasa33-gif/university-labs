using System;
using System.Globalization;

namespace oop_course
{
    public static class Task8
    {
        public static double CalculateBMI(double weight, double height)
        {
            return weight / (height * height);
        }

        public static string GetBMICategory(double bmi)
        {
            if (bmi < 18.5) return "недостатня вага";
            if (bmi < 25.0) return "норма";
            if (bmi < 30.0) return "надмірна вага";
            return "ожиріння";
        }

        public static double CalculateCost(double price, int visits, int discount)
        {
            return price * visits * (1.0 - discount / 100.0);
        }

        public static string GetAgeCategory(int age)
        {
            if (age < 18) return "дитина";
            if (age <= 60) return "дорослий";
            return "пенсіонер";
        }

        public static string GetPressureStatus(int systolic, int diastolic)
        {
            if (systolic < 120 && diastolic < 80) return "нормальний";
            if (systolic <= 129 && diastolic < 80) return "підвищений";
            if (systolic <= 139 || diastolic <= 89) return "гіпертонія 1 ступеня";
            return "гіпертонія 2 ступеня";
        }

        public static void Run()
        {
            double weight = double.Parse(Console.ReadLine()!.Replace(',', '.'), CultureInfo.InvariantCulture);
            double height = double.Parse(Console.ReadLine()!.Replace(',', '.'), CultureInfo.InvariantCulture);
            double price = double.Parse(Console.ReadLine()!.Replace(',', '.'), CultureInfo.InvariantCulture);
            int visits = int.Parse(Console.ReadLine()!);
            int discount = int.Parse(Console.ReadLine()!);
            int birthYear = int.Parse(Console.ReadLine()!);
            int systolic = int.Parse(Console.ReadLine()!);
            int diastolic = int.Parse(Console.ReadLine()!);

            double bmi = CalculateBMI(weight, height);
            string bmiCat = GetBMICategory(bmi);
            double totalCost = CalculateCost(price, visits, discount);
            int age = 2026 - birthYear;
            string ageCat = GetAgeCategory(age);
            string pressureStat = GetPressureStatus(systolic, diastolic);

            Console.WriteLine($"IMT: {bmi:F2} -> {bmiCat}");
            Console.WriteLine($"Сума: {totalCost:F2} грн");
            Console.WriteLine($"Вік: {age} р., категорія: {ageCat}");
            Console.WriteLine($"Тиск: {systolic}/{diastolic} — {pressureStat}");
        }
    }
}