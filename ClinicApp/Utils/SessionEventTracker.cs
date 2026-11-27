
using ClinicApp.Events;

namespace ClinicApp.Utils;

public class SessionEventTracker
{
    public int PatientsAdded { get; private set; }
    public int AppointmentsBooked { get; private set; }
    public int UrgentBooked { get; private set; }
    public int AppointmentsCancelled { get; private set; }
    public int AppointmentsCompleted { get; private set; }
    public int PaymentsReceived { get; private set; }
    public int PlansCompleted { get; private set; }

    public void OnPatientAdded(object? sender, PatientEventArgs e)
        => PatientsAdded++;

    public void OnAppointmentBooked(object? sender, AppointmentEventArgs e)
        => AppointmentsBooked++;

    public void OnUrgentBooked(object? sender, AppointmentEventArgs e)
        => UrgentBooked++;

    public void OnAppointmentCancelled(object? sender, AppointmentEventArgs e)
        => AppointmentsCancelled++;

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e)
        => AppointmentsCompleted++;

    public void OnPaymentReceived(object? sender, PaymentEventArgs e)
        => PaymentsReceived++;

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e)
        => PlansCompleted++;

    public void PrintSummary()
    {
        Console.WriteLine("\n===== СТАТИСТИКА СЕСІЇ =====");
        Console.WriteLine($"Пацієнтів додано: {PatientsAdded}");
        Console.WriteLine($"Записів створено: {AppointmentsBooked}");
        Console.WriteLine($"Термінових записів: {UrgentBooked}");
        Console.WriteLine($"Записів скасовано: {AppointmentsCancelled}");
        Console.WriteLine($"Прийомів завершено: {AppointmentsCompleted}");
        Console.WriteLine($"Оплат отримано: {PaymentsReceived}");
        Console.WriteLine($"Планів завершено: {PlansCompleted}");
        Console.WriteLine($"Дата: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
    }

    public void SaveSummary(string path = "session_summary.txt")
    {
        string summary =
            "===== СТАТИСТИКА СЕСІЇ =====\n" +
            $"Пацієнтів додано: {PatientsAdded}\n" +
            $"Записів створено: {AppointmentsBooked}\n" +
            $"Термінових записів: {UrgentBooked}\n" +
            $"Записів скасовано: {AppointmentsCancelled}\n" +
            $"Прийомів завершено: {AppointmentsCompleted}\n" +
            $"Оплат отримано: {PaymentsReceived}\n" +
            $"Планів завершено: {PlansCompleted}\n" +
            $"Дата: {DateTime.Now:dd.MM.yyyy HH:mm:ss}\n";

        File.WriteAllText(path, summary);
    }
}
