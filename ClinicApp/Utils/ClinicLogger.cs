
using System.Text;
using ClinicApp.Events;
using ClinicApp.Managers;
namespace ClinicApp.Utils;

public class ClinicLogger
{
    private readonly string _logPath;

    public ClinicLogger(string logPath = "clinic.log")
    {
        _logPath = logPath;
    }

    private void Write(string level, string message)
    {
        string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}{Environment.NewLine}";

        File.AppendAllText(_logPath, line, Encoding.UTF8);
    }

    public void LogInfo(string message)
    {
        Write("INFO ", message);
    }

    public void LogWarning(string message)
    {
        Write("WARN ", message);
    }

    public void LogError(string message)
    {
        Write("ERROR", message);
    }

    public string[] GetLastLines(int n)
    {
        if (!File.Exists(_logPath) || n <= 0)
            return Array.Empty<string>();

        string[] lines = File.ReadAllLines(_logPath, Encoding.UTF8);

        int start = Math.Max(0, lines.Length - n);
        int count = lines.Length - start;

        string[] result = new string[count];

        Array.Copy(lines, start, result, 0, count);

        return result;
    }

    public void Clear()
    {
        if (File.Exists(_logPath))
            File.Delete(_logPath);
    }

    public bool Exists()
    {
        return File.Exists(_logPath);
    }
    public void SubscribeEvents(Clinic clinic)
    {
        clinic.Appointments.AppointmentBooked += (sender, e) =>
            LogInfo($"Створено запис #{e.AppointmentId}");

        clinic.Appointments.AppointmentCancelled += (sender, e) =>
            LogInfo($"Скасовано запис #{e.AppointmentId}");

        clinic.Appointments.AppointmentCompleted += (sender, e) =>
            LogInfo($"Завершено запис #{e.AppointmentId}");
        clinic.TreatmentPlans.PlanCompleted += (sender, e) =>
    LogInfo($"Завершено план лікування #{e.PlanId}");

        clinic.Appointments.UrgentAppointmentBooked += (sender, e) =>
        {
            LogWarning($"Терміновий запис #{e.AppointmentId}");

            string folder = "alerts";
            Directory.CreateDirectory(folder);

            string path = Path.Combine(
                folder,
                $"urgent_{DateTime.Now:yyyy-MM-dd}.txt");

            File.AppendAllText(
                path,
                $"[{DateTime.Now:HH:mm:ss}] Терміновий запис #{e.AppointmentId}{Environment.NewLine}");
        };

        clinic.Patients.PatientAdded += (sender, e) =>
            LogInfo($"Додано пацієнта #{e.PatientId}: {e.FullName}");
        clinic.Billing.PaymentReceived += (sender, e) =>
    LogInfo($"Отримано оплату {e.Amount:F2} грн за запис #{e.AppointmentId}");
    }
}
