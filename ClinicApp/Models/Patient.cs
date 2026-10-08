using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class Patient
{
    private static int _nextId = 1;

    public int Id { get; }
   

    private string _firstName = "";
    private string _lastName = "";
    private DateTime _dateOfBirth;
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

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            if (value > DateTime.Today || value < new DateTime(1900, 1, 1))
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    "Дата народження має бути між 1900 роком і сьогодні.");

            _dateOfBirth = value;
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

    public BloodType BloodType { get; set; }


    public string Email { get; set; }


    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }

    public int Age
    {
        get
        {
            int age = DateTime.Today.Year - DateOfBirth.Year;

            if (DateTime.Today < DateOfBirth.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }

    public bool IsAdult
    {
        get
        {
            return Age >= 18;
        }
    }

    public Patient()
        : this("Невідомий", "Пацієнт",
               new DateTime(2000, 1, 1),
               BloodType.Unknown, "0000000000")
    {
    }

    public Patient(string firstName, string lastName)
        : this(firstName, lastName,
               new DateTime(2000, 1, 1),
               BloodType.Unknown, "0000000000")
    {
    }

    public Patient(
        string firstName,
        string lastName,
        DateTime dateOfBirth,
        BloodType bloodType,
        string phone)
    {
        Id = _nextId++;

        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        BloodType = bloodType;
        Phone = phone;
        Email = "";
    }

    public string GetAgeCategory()
    {
        if (Age < 18)
        {
            return "дитина";
        }

        if (Age < 60)
        {
            return "дорослий";
        }

        return "літній";
    }


    public override string ToString()
    {
        return $"[{Id}] {FullName} | " +
               $"Вік: {ClinicFormatter.FormatAge(Age)} " +
               $"({GetAgeCategory()}) | " +
               $"Кров: {ClinicFormatter.FormatBloodType(BloodType)} | " +
               $"Тел: {ClinicFormatter.FormatPhone(Phone)}";
    }

}
