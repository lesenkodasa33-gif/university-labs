
using System;
using System.IO;
using ClinicApp.Models;

namespace ClinicApp.Utils;

public class ClinicExporter
{
    private readonly Clinic _clinic;
    private readonly string _baseFolder;

    public ClinicExporter(Clinic clinic, string baseFolder = "reports")
    {
        _clinic = clinic;
        _baseFolder = baseFolder;
    }

    private string CreateReport(string fileName, string title, int count)
    {
        string dateFolder = DateTime.Now.ToString("yyyy-MM-dd");
        string folderPath = Path.Combine(_baseFolder, dateFolder);

        Directory.CreateDirectory(folderPath);

        string filePath = Path.Combine(folderPath, fileName);

        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine($"ЗВІТ: {title}");
            writer.WriteLine($"Клініка: {_clinic.Name}");
            writer.WriteLine($"Дата генерації: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
            writer.WriteLine("----------------------------------------");
            writer.WriteLine($"Кількість записів: {count}");
            writer.WriteLine();

            if (fileName == "patients.txt")
            {
                for (int i = 0; i < _clinic.Patients.Count; i++)
                    writer.WriteLine(_clinic.Patients[i]);
            }
            else if (fileName == "doctors.txt")
            {
                Doctor[] doctors = _clinic.Doctors.GetAll();

                for (int i = 0; i < doctors.Length; i++)
                    writer.WriteLine(doctors[i]);
            }
            else if (fileName == "appointments.txt")
            {
                Appointment[] appointments = _clinic.Appointments.GetAll();

                for (int i = 0; i < appointments.Length; i++)
                    writer.WriteLine(appointments[i]);
            }
            else if (fileName == "medical_records.txt")
            {
                for (int i = 0; i < _clinic.MedicalRecords.Count; i++)
                    writer.WriteLine(_clinic.MedicalRecords[i]);
            }
        }

        return filePath;
    }

    public string ExportPatients()
    {
        return CreateReport(
            "patients.txt",
            "Пацієнти",
            _clinic.Patients.Count);
    }

    public string ExportDoctors()
    {
        return CreateReport(
            "doctors.txt",
            "Лікарі",
            _clinic.Doctors.Count);
    }

    public string ExportAppointments()
    {
        return CreateReport(
            "appointments.txt",
            "Записи на прийом",
            _clinic.Appointments.Count);
    }

    public string ExportMedicalRecords()
    {
        return CreateReport(
            "medical_records.txt",
            "Медичні записи",
            _clinic.MedicalRecords.Count);
    }

    public string[] ExportAll()
    {
        string[] paths = new string[4];

        paths[0] = ExportPatients();
        paths[1] = ExportDoctors();
        paths[2] = ExportAppointments();
        paths[3] = ExportMedicalRecords();

        return paths;
    }
}
