namespace ClinicApp;

public class AppointmentManager
{
    private const int MaxAppointments = 200;

    private Appointment[] _appointments = new Appointment[MaxAppointments];
    private int _count = 0;

    private PatientManager _patientManager;
    private DoctorManager _doctorManager;

    public int Count
    {
        get
        {
            return _count;
        }
    }

    public AppointmentManager(
        PatientManager patientManager,
        DoctorManager doctorManager)
    {
        _patientManager = patientManager;
        _doctorManager = doctorManager;
    }

    public bool Schedule(
        int patientId,
        int doctorId,
        DateTime dateTime,
        string reason)
    {
        if (_count >= MaxAppointments)
        {
            Console.WriteLine("Досягнуто ліміт записів.");
            return false;
        }

        Patient? patient = _patientManager.FindById(patientId);

        if (patient == null)
        {
            Console.WriteLine("Пацієнта з таким ID не знайдено.");
            return false;
        }

        Doctor? doctor = _doctorManager.FindById(doctorId);

        if (doctor == null)
        {
            Console.WriteLine("Лікаря з таким ID не знайдено.");
            return false;
        }

        if (!doctor.CanAcceptAt(dateTime.Hour))
        {
            Console.WriteLine("Лікар не працює у вказану годину.");
            return false;
        }

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId &&
                _appointments[i].DateTime == dateTime &&
                _appointments[i].Status == "Scheduled")
            {
                Console.WriteLine("На цей час у лікаря вже є запис.");
                return false;
            }
        }

        Appointment appointment =
            new Appointment(patientId, doctorId, dateTime, reason);

        _appointments[_count] = appointment;
        _count++;

        Console.WriteLine("Запис успішно створено.");
        return true;
    }

    public Appointment? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].Id == id)
            {
                return _appointments[i];
            }
        }

        return null;
    }

    public Appointment[] GetByPatient(int patientId)
    {
        int matches = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                matches++;
            }
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                result[index] = _appointments[i];
                index++;
            }
        }

        return result;
    }

    public Appointment[] GetByDoctor(int doctorId)
    {
        int matches = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                matches++;
            }
        }

        Appointment[] result = new Appointment[matches];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                result[index] = _appointments[i];
                index++;
            }
        }

        return result;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Записів немає.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"=== Записи ({_count}) ===");

        for (int i = 0; i < _count; i++)
        {
            DisplayAppointment(_appointments[i]);
        }
    }

    public void DisplayUpcoming()
    {
        Console.WriteLine();
        Console.WriteLine("=== Майбутні записи ===");

        bool found = false;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                DisplayAppointment(_appointments[i]);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Майбутніх записів немає.");
        }
    }

    public bool Cancel(int id)
    {
        Appointment? appointment = FindById(id);

        if (appointment == null)
        {
            return false;
        }

        return appointment.Cancel();
    }

    public bool Complete(int id)
    {
        Appointment? appointment = FindById(id);

        if (appointment == null)
        {
            return false;
        }

        return appointment.Complete();
    }

    private void DisplayAppointment(Appointment appointment)
    {
        Patient? patient =
            _patientManager.FindById(appointment.PatientId);

        Doctor? doctor =
            _doctorManager.FindById(appointment.DoctorId);

        string patientName =
            patient != null ? patient.FullName : "Невідомий пацієнт";

        string doctorName =
            doctor != null ? doctor.FullName : "Невідомий лікар";

        Console.WriteLine(
            $"[A{appointment.Id}] {patientName} → {doctorName} | " +
            $"{appointment.DateTime:dd.MM.yyyy HH:mm} | " +
            $"{appointment.Reason} | {appointment.Status}");
    }
}