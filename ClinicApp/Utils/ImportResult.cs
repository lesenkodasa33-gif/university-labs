
using System.Collections.Generic;

namespace ClinicApp.Utils;

public class ImportResult
{
    public int ImportedCount { get; private set; }

    public int SkippedCount { get; private set; }

    public List<string> Errors { get; } = new List<string>();

    public void AddImported()
    {
        ImportedCount++;
    }

    public void AddError(int lineNumber, string reason)
    {
        SkippedCount++;
        Errors.Add($"Рядок {lineNumber}: {reason}");
    }

    public void DisplaySummary()
    {
        Console.WriteLine("\n=== Результат імпорту ===");
        Console.WriteLine($"Успішно імпортовано: {ImportedCount}");
        Console.WriteLine($"Пропущено рядків: {SkippedCount}");

        if (Errors.Count > 0)
        {
            Console.WriteLine("\nПомилки:");

            for (int i = 0; i < Errors.Count; i++)
            {
                Console.WriteLine(Errors[i]);
            }
        }
    }
}
