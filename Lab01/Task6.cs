using System;
// Task 6 у Task6.cs
namespace oop_course
{
    public static class Task6
    {
        public static void Run()
        {
            long number = long.Parse(Console.ReadLine()!);

            long lastDigit = Math.Abs(number % 10);
            string department = lastDigit switch
            {
                0 or 1 => "відділення: загальна терапія",
                2 or 3 => "відділення: хірургія",
                4 or 5 => "відділення: кардіологія",
                6 or 7 => "відділення: неврологія",
                8 or 9 => "відділення: офтальмологія",
                _ => ""
            };

            string isPrivileged = (number % 2 == 0) ? "пільгова картка: так" : "пільгова картка: ні";
            string isDuty = (number % 3 == 0) ? "черговий огляд: так" : "черговий огляд: ні";

            Console.WriteLine(department);
            Console.WriteLine(isPrivileged);
            Console.WriteLine(isDuty);
        }
    }
}