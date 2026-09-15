using System;
using System.Globalization;
// Task 8 у Task8.cs
namespace oop_course
{
    public static class Task8
    {
        public static void Run()
        {
            
            double weight = double.Parse(Console.ReadLine()!.Replace(',', '.'), CultureInfo.InvariantCulture);
            double height = double.Parse(Console.ReadLine()!.Replace(',', '.'), CultureInfo.InvariantCulture);
            double bmi = CalculateBMI(weight, height);
            string bmiCategory = GetBMICategory(bmi);
            Console.WriteLine(bmiCategory);

            double price = double.Parse(Console.ReadLine()!.Replace(',', '.'), CultureInfo.InvariantCulture);
            int count = int.Parse(Console.ReadLine()!);
            int discount = int.Parse(Console.ReadLine()!);
            double cost = CalculateCost(price, count, discount);
            Console.WriteLine(cost);

            
            int birthYear = int.Parse(Console.ReadLine()!);
            string ageCategory = GetAgeCategory(2026 - birthYear);
            Console.WriteLine(ageCategory);

            
            int systolic = int.Parse(Console.ReadLine()!);
            int diastolic = int.Parse(Console.ReadLine()!);
            string pressureStatus = GetPressureStatus(systolic, diastolic);
            Console.WriteLine(pressureStatus);
        }

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

        public static double CalculateCost(double price, int count, int discount)
        {
            return price * count * (1.0 - discount / 100.0);
        }

        public static string GetAgeCategory(int age)
        {
            if (age <= 17) return "дитина";
            if (age <= 59) return "дорослий";
            return "пенсіонер";
        }

        public static string GetPressureStatus(int systolic, int diastolic)
        {
            if (systolic < 120 && diastolic < 80) return "норма";
            if (systolic < 130 && diastolic < 80) return "підвищений";
            if (systolic < 140 || diastolic < 90) return "гіпертонія 1 ступеня";
            return "гіпертонія 2 ступеня";
        }
    }
}