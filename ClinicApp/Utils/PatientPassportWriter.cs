
using System.Text;
using ClinicApp;
using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class PatientPassportWriter
{
    private readonly Clinic _clinic;
    private readonly string _folder;

    public PatientPassportWriter(
        Clinic clinic,
        string folder = "patients")
    {
        _clinic = clinic;
        _folder = folder;

        Directory.CreateDirectory(_folder);
    }

    public void OnPatientAdded(object? sender, PatientEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void OnAppointmentCompleted(
        object? sender,
        AppointmentEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void OnPlanCompleted(
        object? sender,
        TreatmentPlanEventArgs e)
    {
        var plan = _clinic.TreatmentPlans.GetById(e.PlanId);

        if (plan != null)
            WritePassport(plan.PatientId);
    }

    private void WritePassport(int patientId)
    {
        Patient? patient = _clinic.Patients.FindById(patientId);

        if (patient == null)
            return;

        StringBuilder sb = new();

        sb.AppendLine("========== МЕДИЧНИЙ ПАСПОРТ ==========");
        sb.AppendLine();
        sb.AppendLine("1. ОСОБИСТІ ДАНІ");
        sb.AppendLine($"ID: {patient.Id}");
        sb.AppendLine($"ПІБ: {patient.FullName}");
        sb.AppendLine($"Дата народження: {patient.DateOfBirth:dd.MM.yyyy}");
        sb.AppendLine($"Вік: {patient.Age}");
        sb.AppendLine($"Телефон: {patient.Phone}");
        sb.AppendLine($"Email: {patient.Email}");
        sb.AppendLine($"Група крові: {patient.BloodType}");

        sb.AppendLine();
        sb.AppendLine("2. МЕДИЧНІ ЗАПИСИ");

        foreach (var record in _clinic.MedicalRecords.GetByPatient(patientId))
            sb.AppendLine(record.ToString());

        sb.AppendLine();
        sb.AppendLine("3. ПРИЙОМИ");

        foreach (var appointment in _clinic.Appointments.GetByPatient(patientId))
            sb.AppendLine(appointment.ToString());

        sb.AppendLine();
        sb.AppendLine("4. ПЛАНИ ЛІКУВАННЯ");

        foreach (var plan in _clinic.TreatmentPlans.GetByPatient(patientId))
        {
            sb.AppendLine(
                $"План #{plan.Id}: {plan.Description} | " +
                $"Завершено: {plan.IsCompleted}");
        }

        sb.AppendLine();
        sb.AppendLine("5. ФІНАНСОВА ІНФОРМАЦІЯ");

        decimal debt = 0;

        foreach (var appointment in _clinic.Appointments.GetByPatient(patientId))
        {
            if (!appointment.IsPaid && !appointment.IsCancelled)
                debt += appointment.GetCost();
        }

        sb.AppendLine($"Заборгованість: {debt:F2} грн");
        sb.AppendLine();
        sb.AppendLine($"Дата формування: {DateTime.Now:dd.MM.yyyy HH:mm}");
        sb.AppendLine("======================================");

        string path = Path.Combine(
            _folder,
            $"passport_{patientId}.txt");

        File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
    }
}
