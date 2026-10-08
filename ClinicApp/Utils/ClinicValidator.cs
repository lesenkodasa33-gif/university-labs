using System.Text.RegularExpressions;

namespace ClinicApp.Utils;

public static class ClinicValidator
{
    private static readonly Regex PhoneRegex =
        new Regex(@"^[0-9]{10}$");

    private static readonly Regex EmailRegex =
        new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    public static void ValidateName(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
        {
            throw new ArgumentException(
                $"{fieldName} має містити від 1 до 50 символів.",
                fieldName);
        }
    }

    public static void ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone) ||
            !PhoneRegex.IsMatch(phone))
        {
            throw new ArgumentException(
                "Телефон повинен містити рівно 10 цифр.",
                nameof(phone));
        }
    }

    public static void ValidateDate(DateTime value, string fieldName)
    {
        if (value > DateTime.Today ||
            value < new DateTime(1900, 1, 1))
        {
            throw new ArgumentOutOfRangeException(
                fieldName,
                "Дата має бути між 1900 роком і сьогодні.");
        }
    }

    public static void ValidatePositive(int value, string fieldName)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(
                fieldName,
                "Значення повинно бути більше 0.");
        }
    }

    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return;

        if (!EmailRegex.IsMatch(email))
        {
            throw new ArgumentException(
                "Некоректний формат email.",
                nameof(email));
        }
    }
}