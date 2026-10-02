
namespace ClinicApp;

public static class ClinicFormatter
{
    public static string FormatBloodType(BloodType bt)
    {
        return bt switch
        {
            BloodType.APositive => "A+",
            BloodType.ANegative => "A-",
            BloodType.BPositive => "B+",
            BloodType.BNegative => "B-",
            BloodType.ABPositive => "AB+",
            BloodType.ABNegative => "AB-",
            BloodType.OPositive => "O+",
            BloodType.ONegative => "O-",
            _ => "Невідомо"
        };
    }

    public static string FormatSpecialty(Speciality s)
    {
        return s switch
        {
            Speciality.General => "Загальна медицина",
            Speciality.Cardiology => "Кардіологія",
            Speciality.Neurology => "Неврологія",
            Speciality.Pediatrics => "Педіатрія",
            Speciality.Surgery => "Хірургія",
            Speciality.Orthopedics => "Ортопедія",
            Speciality.Dermatology => "Дерматологія",
            Speciality.Emergency => "Невідкладна допомога",
            _ => "Невідомо"
        };
    }

    public static string FormatAge(int age)
    {
        int lastTwo = age % 100;
        int lastOne = age % 10;

        if (lastTwo >= 11 && lastTwo <= 14)
        {
            return $"{age} років";
        }

        if (lastOne == 1)
        {
            return $"{age} рік";
        }

        if (lastOne >= 2 && lastOne <= 4)
        {
            return $"{age} роки";
        }

        return $"{age} років";
    }

    public static string FormatPhone(string phone)
    {
        string digits = new string(
            phone.Where(char.IsDigit).ToArray());

        if (digits.Length == 10)
        {
            return $"({digits[..3]}) " +
                   $"{digits.Substring(3, 3)}-" +
                   $"{digits.Substring(6, 4)}";
        }

        return phone;
    }
}
