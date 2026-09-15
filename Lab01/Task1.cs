using System;

class Task1
{
    static void Main()
    {
        double weight = double.Parse(Console.ReadLine());
        double height = double.Parse(Console.ReadLine());

        double bmi = weight / (height * height);

        Console.WriteLine(bmi);
    }
}