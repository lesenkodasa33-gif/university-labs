namespace ClinicApp;

public class PatientManager
{
    private const int MaxPatients = 100;
    private Patient[] _patients = new Patient[MaxPatients];
    private int _count = 0;

    public int Count
    {
        get
        {
            return _count;
        }
    }


    public Patient? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                return null;
            }

            return _patients[index];
        }
    }


    public void Add(Patient patient)
    {
        if (_count >= MaxPatients)
        {
            Console.WriteLine("Досягнуто ліміт пацієнтів.");
            return;
        }

        _patients[_count] = patient;
        _count++;

        Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FullName} додано.");
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
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
        int foundCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                foundCount++;
            }
        }

        Patient[] result = new Patient[foundCount];
        int resultIndex = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                result[resultIndex] = _patients[i];
                resultIndex++;
            }
        }

        return result;
    }

    public Patient[] FindByName(string name)
    {
        string search = name.ToLower();
        int matches = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(search) ||
                _patients[i].LastName.ToLower().Contains(search))
            {
                matches++;
            }
        }

        Patient[] result = new Patient[matches];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].FirstName.ToLower().Contains(search) ||
                _patients[i].LastName.ToLower().Contains(search))
            {
                result[index] = _patients[i];
                index++;
            }
        }

        return result;
    }

    public bool Remove(int id)
    {
        int index = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            return false;
        }

        for (int i = index; i < _count - 1; i++)
        {
            _patients[i] = _patients[i + 1];
        }

        _count--;

        return true;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Список пацієнтів порожній.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"=== Пацієнти ({_count} / {MaxPatients}) ===");

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_patients[i]);
        }

        Console.WriteLine("----------------------------------------");
    }

    public void DisplayStats()
    {
        if (_count == 0)
        {
            Console.WriteLine("Список пацієнтів порожній.");
            return;
        }

        int totalAge = 0;
        int youngestIndex = 0;
        int oldestIndex = 0;
        int adults = 0;

        for (int i = 0; i < _count; i++)
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

        double averageAge = (double)totalAge / _count;

        Console.WriteLine();
        Console.WriteLine("=== Статистика пацієнтів ===");
        Console.WriteLine($"Всього:       {_count}");
        Console.WriteLine($"Середній вік: {averageAge:F1} р.");
        Console.WriteLine(
            $"Наймолодший:  {_patients[youngestIndex].FullName} " +
            $"({_patients[youngestIndex].Age} р.)");
        Console.WriteLine(
            $"Найстарший:   {_patients[oldestIndex].FullName} " +
            $"({_patients[oldestIndex].Age} р.)");
        Console.WriteLine($"Дорослих:     {adults} з {_count}");
        Console.WriteLine("============================");
    }
}