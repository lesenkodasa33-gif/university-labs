namespace ClinicApp;

public class Appointment
{
    private static int _nextId = 1;
    private string _status;

    public int Id { get; }

    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime DateTime { get; set; }
    public string Reason { get; set; }

    public string Status
    {
        get
        {
            return _status;
        }
    }

    public bool IsUpcoming
    {
        get
        {
            return _status == "Scheduled" &&
                   DateTime > System.DateTime.Now;
        }
    }

    public Appointment(
        int patientId,
        int doctorId,
        DateTime dateTime,
        string reason)
    {
        Id = _nextId;
        _nextId++;

        PatientId = patientId;
        DoctorId = doctorId;
        DateTime = dateTime;
        Reason = reason;

        _status = "Scheduled";
    }

    public bool Cancel()
    {
        if (_status != "Scheduled")
        {
            return false;
        }

        _status = "Cancelled";
        return true;
    }

    public bool Complete()
    {
        if (_status != "Scheduled")
        {
            return false;
        }

        _status = "Completed";
        return true;
    }

    public override string ToString()
    {
        return $"[A{Id}] Пацієнт #{PatientId} → Лікар #{DoctorId} | " +
               $"{DateTime:dd.MM.yyyy HH:mm} | {Reason} | {_status}";
    }
}