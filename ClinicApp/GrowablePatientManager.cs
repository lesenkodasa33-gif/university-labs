namespace ClinicApp;

public class GrowablePatientManager
{
    private Patient[] _patients;
    private int _count;

    public int Count
    {
        get
        {
            return _count;
        }
    }

    public int Capacity
    {
        get
        {
            return _patients.Length;
        }
    }

    public GrowablePatientManager()
    {
        _patients = new Patient[2];
        _count = 0;
    }

    public void Add(Patient patient)
    {
        if (_count == _patients.Length)
        {
            Grow();
        }

        _patients[_count] = patient;
        _count++;

        Console.WriteLine(
            $"Пацієнта [{patient.Id}] {patient.FullName} додано. " +
            $"Count: {_count}, Capacity: {_patients.Length}");
    }

    private void Grow()
    {
        int newSize = _patients.Length * 2;

        Patient[] newPatients = new Patient[newSize];

        for (int i = 0; i < _count; i++)
        {
            newPatients[i] = _patients[i];
        }

        _patients = newPatients;

        Console.WriteLine(
            $"Масив збільшено до {_patients.Length} елементів.");
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

    public void DisplayAll()
    {
        Console.WriteLine();
        Console.WriteLine(
            $"=== Пацієнти: {_count}, місткість: {_patients.Length} ===");

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_patients[i]);
        }
    }
}