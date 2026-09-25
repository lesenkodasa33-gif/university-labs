namespace ClinicApp;

public class Clinic
{
    public PatientManager Patients { get; }
    public DoctorManager Doctors { get; }
    public AppointmentManager Appointments { get; }

    public Clinic()
    {
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager(Patients, Doctors);
    }

    public void DisplaySchedule()
    {
        Console.WriteLine();
        Console.WriteLine("=== Розклад клініки ===");

        Appointments.DisplayUpcoming();
    }

    public void GenerateReport()
    {
        Console.WriteLine();
        Console.WriteLine("========== ЗВІТ КЛІНІКИ ==========");

        Console.WriteLine();
        Console.WriteLine("--- Пацієнти ---");
        Patients.DisplayStats();

        Console.WriteLine();
        Console.WriteLine("--- Лікарі ---");
        Doctors.DisplayStats();

        Console.WriteLine();
        Console.WriteLine("--- Записи ---");
        Appointments.DisplayAll();

        Console.WriteLine();
        Console.WriteLine("==================================");
    }
}