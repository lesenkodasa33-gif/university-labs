using ClinicApp.Enums;
using ClinicApp.Models;
using System;
using System.IO;
using System.Text;


namespace ClinicApp.Utils;

public class SessionManager
{
    private readonly string _sessionPath;

    public SessionManager(string sessionPath = "session.dat")
    {
        _sessionPath = sessionPath;
    }

    public bool Exists()
    {
        return File.Exists(_sessionPath);
    }

    public void Save(Clinic clinic)
    {
        using StreamWriter writer = new StreamWriter(
            _sessionPath, false, Encoding.UTF8);

        writer.WriteLine("[PATIENTS]");

        for (int i = 0; i < clinic.Patients.Count; i++)
        {
            Patient? patient = clinic.Patients[i];

            if (patient == null)
                continue;

            writer.WriteLine(
                $"{patient.FirstName},{patient.LastName}," +
                $"{patient.DateOfBirth:dd.MM.yyyy}," +
                $"{patient.BloodType},{patient.Phone}");
        }

        writer.WriteLine("[END]");
    }
    public int Load(Clinic clinic)
    {
        if (!File.Exists(_sessionPath))
            return 0;

        string[] lines = File.ReadAllLines(_sessionPath);

        bool isPatientsSection = false;
        int loadedCount = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (line.StartsWith("["))
            {
                if (line == "[PATIENTS]")
                    isPatientsSection = true;
                else
                    isPatientsSection = false;

                if (line == "[END]")
                    break;

                continue;
            }

            if (!isPatientsSection)
                continue;

            try
            {
                string[] parts = line.Split(',');

                if (parts.Length != 5)
                    continue;

                string firstName = parts[0].Trim();
                string lastName = parts[1].Trim();

                DateTime dateOfBirth = DateTime.ParseExact(
                    parts[2].Trim(),
                    "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture);

                BloodType bloodType = (BloodType)Enum.Parse(
                    typeof(BloodType), parts[3].Trim(), true);

                string phone = parts[4].Trim();

                bool alreadyExists = false;

                for (int j = 0; j < clinic.Patients.Count; j++)
                {
                    Patient? existing = clinic.Patients[j];

                    if (existing != null &&
                        existing.FirstName == firstName &&
                        existing.LastName == lastName &&
                        existing.DateOfBirth == dateOfBirth)
                    {
                        alreadyExists = true;
                        break;
                    }
                }

                if (alreadyExists)
                    continue;

                Patient patient = new Patient(
                    firstName, lastName, dateOfBirth, bloodType, phone);

                clinic.Patients.Add(patient);
                loadedCount++;
            }
            catch (Exception)
            {
                // Пошкоджений рядок пропускаємо
            }
        }

        return loadedCount;
    }
}