using ClinicApp;

Patient patient1 = new Patient(
    "Іван",
    "Петренко",
    new DateTime(1985, 5, 15),
    "A+",
    "0501234567"
);

Patient patient2 = new Patient(
    "Олена",
    "Коваль",
    new DateTime(1993, 3, 10),
    "B-",
    "0672345678"
);

Patient patient3 = new Patient(
    "Максим",
    "Бойко",
    new DateTime(2010, 7, 20),
    "O+",
    "0933456789"
);

Patient patient4 = new Patient();

Patient patient5 = new Patient("Марія", "Ткач");

Console.WriteLine(patient1);
Console.WriteLine(patient2);
Console.WriteLine(patient3);
Console.WriteLine(patient4);
Console.WriteLine(patient5);
Console.WriteLine();
Console.WriteLine("=== Лікарі ===");

Doctor doctor1 = new Doctor(
    "Олег",
    "Сидоренко",
    "Кардіологія",
    "LIC-001",
    "0441234567"
);

doctor1.WorkStartHour = 8;
doctor1.WorkEndHour = 16;

Doctor doctor2 = new Doctor(
    "Наталія",
    "Мороз",
    "Неврологія",
    "LIC-002",
    "0442345678"
);

doctor2.WorkStartHour = 9;
doctor2.WorkEndHour = 18;

Doctor doctor3 = new Doctor(
    "Андрій",
    "Власенко",
    "Педіатрія"
);

Console.WriteLine(doctor1);
Console.WriteLine(doctor2);
Console.WriteLine(doctor3);
PatientManager patientManager = new PatientManager();

patientManager.Add(patient1);
patientManager.Add(patient2);
patientManager.Add(patient3);
patientManager.Add(patient4);
patientManager.Add(patient5);

PatientMenu(patientManager);

static void PatientMenu(PatientManager manager)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("=== Пацієнти ===");
        Console.WriteLine("1. Показати всіх");
        Console.WriteLine("2. Додати");
        Console.WriteLine("3. Знайти за ім'ям");
        Console.WriteLine("4. Видалити");
        Console.WriteLine("5. Статистика");
        Console.WriteLine("0. Вийти");
        Console.Write("Ваш вибір: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                manager.DisplayAll();
                break;

            case "2":
                Console.Write("Ім'я: ");
                string firstName = Console.ReadLine()!;

                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine()!;

                Patient patient = new Patient(firstName, lastName);
                manager.Add(patient);
                break;

            case "3":
                Console.Write("Введіть ім'я або прізвище: ");
                string name = Console.ReadLine()!;

                Patient[] found = manager.FindByName(name);

                if (found.Length == 0)
                {
                    Console.WriteLine("Пацієнтів не знайдено.");
                }
                else
                {
                    for (int i = 0; i < found.Length; i++)
                    {
                        Console.WriteLine(found[i]);
                    }
                }

                break;

            case "4":
                Console.Write("Введіть ID пацієнта: ");

                int id;

                if (int.TryParse(Console.ReadLine(), out id))
                {
                    if (manager.Remove(id))
                    {
                        Console.WriteLine("Пацієнта видалено.");
                    }
                    else
                    {
                        Console.WriteLine("Пацієнта не знайдено.");
                    }
                }
                else
                {
                    Console.WriteLine("Некоректний ID.");
                }

                break;

            case "5":
                manager.DisplayStats();
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}