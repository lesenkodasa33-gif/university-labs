namespace ClinicApp.Utils;

public static class ClinicValidator
{
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
        if (string.IsNullOrWhiteSpace(phone) || phone.Length != 10)
        {
            throw new ArgumentException(
                "Телефон повинен містити рівно 10 цифр.",
                nameof(phone));
        }

        foreach (char c in phone)
        {
            if (!char.IsDigit(c))
            {
                throw new ArgumentException(
                    "Телефон повинен містити лише цифри.",
                    nameof(phone));
            }
        }
    }

    public static void ValidateDate(DateTime value, string fieldName)
    {
        if (value > DateTime.Today || value < new DateTime(1900, 1, 1))
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
}