
using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class PatientManager
{
    private List<Patient> _patients = new List<Patient>();

    public int Count
    {
        get
        {
            return _patients.Count;
        }
    }

    public Patient? this[int index]
    {
        get
        {
            if (index < 0 || index >= _patients.Count)
            {
                return null;
            }

            return _patients[index];
        }
    }

    public void Add(Patient patient)
    {
        _patients.Add(patient);

        Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FullName} додано.");
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].Id == id)
            {
                return _patients[i];
            }
        }

        return null;
    }

    public bool TryFindById(int id, out Patient patient)
    {
        Patient? found = FindById(id);

        if (found != null)
        {
            patient = found;
            return true;
        }

        patient = null!;
        return false;
    }

    public Patient[] FindByBloodType(BloodType bloodType)
    {
        List<Patient> result = new List<Patient>();

        foreach (Patient patient in _patients)
        {
            if (patient.BloodType == bloodType)
            {
                result.Add(patient);
            }
        }

        return result.ToArray();
    }

    public Patient[] FindByName(string name)
    {
        string search = name.ToLower();
        List<Patient> result = new List<Patient>();

        foreach (Patient patient in _patients)
        {
            if (patient.FirstName.ToLower().Contains(search) ||
                patient.LastName.ToLower().Contains(search))
            {
                result.Add(patient);
            }
        }

        return result.ToArray();
    }

    public bool Remove(int id)
    {
        for (int i = 0; i < _patients.Count; i++)
        {
            if (_patients[i].Id == id)
            {
                _patients.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public Patient[] GetAll()
    {
        return _patients.ToArray();
    }

    public void DisplayAll()
    {
        if (_patients.Count == 0)
        {
            Console.WriteLine("Список пацієнтів порожній.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"=== Пацієнти ({_patients.Count}) ===");

        foreach (Patient patient in _patients)
        {
            Console.WriteLine(patient);
        }

        Console.WriteLine("----------------------------------------");
    }

    public void DisplayStats()
    {
        if (_patients.Count == 0)
        {
            Console.WriteLine("Список пацієнтів порожній.");
            return;
        }

        int totalAge = 0;
        int youngestIndex = 0;
        int oldestIndex = 0;
        int adults = 0;

        for (int i = 0; i < _patients.Count; i++)
        {
            totalAge += _patients[i].Age;

            if (_patients[i].Age < _patients[youngestIndex].Age)
            {
                youngestIndex = i;
            }

            if (_patients[i].Age > _patients[oldestIndex].Age)
            {
                oldestIndex = i;
            }

            if (_patients[i].IsAdult)
            {
                adults++;
            }
        }

        double averageAge = (double)totalAge / _patients.Count;

        Console.WriteLine();
        Console.WriteLine("=== Статистика пацієнтів ===");
        Console.WriteLine($"Всього:       {_patients.Count}");
        Console.WriteLine($"Середній вік: {averageAge:F1} р.");
        Console.WriteLine(
            $"Наймолодший:  {_patients[youngestIndex].FullName} " +
            $"({_patients[youngestIndex].Age} р.)");
        Console.WriteLine(
            $"Найстарший:   {_patients[oldestIndex].FullName} " +
            $"({_patients[oldestIndex].Age} р.)");
        Console.WriteLine($"Дорослих:     {adults} з {_patients.Count}");
        Console.WriteLine("============================");
    }
}
