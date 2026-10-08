using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Doctor
{
    private static int _nextId = 1;

    public int Id { get; }

    
    public Speciality Speciality { get; set; }
    private string _firstName = "";
    private string _lastName = "";
    private string _licenseNumber = "";
    private string _phone = "";

    public string FirstName
    {
        get => _firstName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
                throw new ArgumentException("Ім'я має містити від 1 до 50 символів.");

            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
                throw new ArgumentException("Прізвище має містити від 1 до 50 символів.");

            _lastName = value;
        }
    }

    public string LicenseNumber
    {
        get => _licenseNumber;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Номер ліцензії не може бути порожнім.");

            _licenseNumber = value;
        }
    }

    public string Phone
    {
        get => _phone;
        set
        {
            if (value == null || value.Length != 10 || !value.All(char.IsDigit))
                throw new ArgumentException("Телефон повинен містити рівно 10 цифр.");

            _phone = value;
        }
    }
    public WorkSchedule Schedule { get; set; }

    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }

    public int WorkingHoursPerDay
    {
        get
        {
            return Schedule.HoursPerDay;
        }
    }

    public bool IsAvailableNow
    {
        get
        {
            return Schedule.IsNow;
        }
    }

    public Doctor()
        : this("Невідомий", "Лікар",
               Speciality.General,
               "N/A", "0000000000")
    {
    }

    public Doctor(
        string firstName,
        string lastName,
        Speciality speciality)
        : this(firstName, lastName, speciality,
               "N/A", "0000000000")
    {
    }

    public Doctor(
        string firstName,
        string lastName,
        Speciality speciality,
        string licenseNumber,
        string phone)
    {
        Id = _nextId++;

        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;

        Schedule = new WorkSchedule(8, 17);
    }

    public bool CanAcceptAt(int hour)
    {
        return Schedule.Contains(hour);
    }


    public override string ToString()
    {
        string status = IsAvailableNow
            ? "доступний зараз"
            : "не в робочий час";

        return $"[{Id}] {FullName} | " +
               $"{ClinicFormatter.FormatSpecialty(Speciality)} | " +
               $"{LicenseNumber} | " +
               $"Тел: {ClinicFormatter.FormatPhone(Phone)} | " +
               $"{Schedule.Display} " +
               $"({WorkingHoursPerDay} год) | {status}";
    }

}
