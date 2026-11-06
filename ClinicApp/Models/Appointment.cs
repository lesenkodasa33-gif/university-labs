
using ClinicApp.Enums;
using ClinicApp.Utils;
using ClinicApp.Interfaces;

namespace ClinicApp.Models;

public class Appointment : IPayable, ICancellable, IIdentifiable
{
    private static int _nextId = 1;
    private bool _isPaid = false;

    public int Id { get; }

    public int PatientId { get; }

    public int DoctorId { get; }

    public DateTime ScheduledAt { get; set; }

    private int _durationMinutes;

    public int DurationMinutes
    {
        get => _durationMinutes;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(DurationMinutes),
                    "Тривалість повинна бути більшою за 0.");
            }

            _durationMinutes = value;
        }
    }

    public AppointmentStatus Status { get; private set; }

    public string Notes { get; private set; }
    public virtual decimal GetCost()
    {
        return DurationMinutes * 10m;
    }

    public virtual string GetDescription()
    {
        return "Прийом";
    }

    public int GetPriority()
    {
        return 3;
    }
    public DateTime EndsAt
    {
        get
        {
            return ScheduledAt.AddMinutes(DurationMinutes);
        }
    }

    public bool IsUpcoming
    {
        get
        {
            return ScheduledAt > DateTime.Now &&
                   Status == AppointmentStatus.Scheduled;
        }
    }

    // Завдання 1: оплата
    public bool IsPaid => _isPaid;

    public bool IsCancelled =>
        Status == AppointmentStatus.Cancelled;

    public string CancellationReason =>
        IsCancelled ? Notes : "";

   


    public void MarkPaid()
    {
        if (IsCancelled)
        {
            return;
        }

        _isPaid = true;
    }


    public Appointment(
        int patientId,
        int doctorId,
        DateTime scheduledAt,
        int durationMinutes = 30)
    {
        Id = _nextId++;

        PatientId = patientId;
        DoctorId = doctorId;

        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;

        Status = AppointmentStatus.Scheduled;
        Notes = "";
    }

    public bool Cancel(string reason = "")
    {
        if (Status != AppointmentStatus.Scheduled)
        {
            return false;
        }

        Status = AppointmentStatus.Cancelled;

        if (reason != "")
        {
            Notes = reason;
        }

        return true;
    }

    public bool Complete()
    {
        if (Status != AppointmentStatus.Scheduled)
        {
            return false;
        }

        Status = AppointmentStatus.Completed;

        return true;
    }

    public override string ToString()
    {
        string result =
            $"[{Id}] {GetDescription()} | Пацієнт #{PatientId} → Лікар #{DoctorId} | " +
            $"{ScheduledAt:dd.MM.yyyy HH:mm}–{EndsAt:HH:mm} | {Status}";

        if (Notes.Length > 0)
        {
            result += $" | {Notes}";
        }

        result += $" | {GetCost():F2} грн";

        return result;
    }
}
