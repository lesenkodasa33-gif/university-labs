using ClinicApp;


// ==================== CLINIC ====================

Clinic clinic = new Clinic();


// ==================== ПАЦІЄНТИ ====================

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

clinic.Patients.Add(patient1);
clinic.Patients.Add(patient2);
clinic.Patients.Add(patient3);
clinic.Patients.Add(patient4);
clinic.Patients.Add(patient5);


// ==================== ЛІКАРІ ====================

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

clinic.Doctors.Add(doctor1);
clinic.Doctors.Add(doctor2);
clinic.Doctors.Add(doctor3);


// ==================== ГОЛОВНЕ МЕНЮ ====================

while (true)
{
    Console.WriteLine();
    Console.WriteLine("========== КЛІНІКА ==========");
    Console.WriteLine("1. Пацієнти");
    Console.WriteLine("2. Лікарі");
    Console.WriteLine("3. Записи");
    Console.WriteLine("4. Розклад");
    Console.WriteLine("5. Звіт");
    Console.WriteLine("6. Зростаючий масив");
    Console.WriteLine("0. Вийти");
    Console.Write("Ваш вибір: ");

    string choice = Console.ReadLine()!;

    switch (choice)
    {
        case "1":
            PatientMenu(clinic.Patients);
            break;

        case "2":
            DoctorMenu(clinic.Doctors);
            break;

        case "3":
            AppointmentMenu(clinic.Appointments);
            break;

        case "4":
            clinic.DisplaySchedule();
            break;

        case "5":
            clinic.GenerateReport();
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Невірний вибір.");
            break;
    }
}


// ==================== PATIENT MENU ====================

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
        Console.WriteLine("0. Назад");
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

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (manager.Remove(id))
                        Console.WriteLine("Пацієнта видалено.");
                    else
                        Console.WriteLine("Пацієнта не знайдено.");
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


// ==================== DOCTOR MENU ====================

static void DoctorMenu(DoctorManager manager)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("=== Лікарі ===");
        Console.WriteLine("1. Показати всіх");
        Console.WriteLine("2. Знайти за спеціальністю");
        Console.WriteLine("3. Видалити");
        Console.WriteLine("4. Статистика");
        Console.WriteLine("5. Перевірити доступність");
        Console.WriteLine("0. Назад");
        Console.Write("Ваш вибір: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                manager.DisplayAll();
                break;

            case "2":
                Console.Write("Введіть спеціальність: ");
                string speciality = Console.ReadLine()!;

                Doctor[] found = manager.FindBySpeciality(speciality);

                if (found.Length == 0)
                {
                    Console.WriteLine("Лікарів не знайдено.");
                }
                else
                {
                    for (int i = 0; i < found.Length; i++)
                    {
                        Console.WriteLine(found[i]);
                    }
                }

                break;

            case "3":
                Console.Write("Введіть ID лікаря: ");

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    if (manager.Remove(id))
                        Console.WriteLine("Лікаря видалено.");
                    else
                        Console.WriteLine("Лікаря не знайдено.");
                }
                else
                {
                    Console.WriteLine("Некоректний ID.");
                }

                break;

            case "4":
                manager.DisplayStats();
                break;

            case "5":
                Console.Write("Введіть ID лікаря: ");

                if (!int.TryParse(Console.ReadLine(), out int doctorId))
                {
                    Console.WriteLine("Некоректний ID.");
                    break;
                }

                Doctor? doctor = manager.FindById(doctorId);

                if (doctor == null)
                {
                    Console.WriteLine("Лікаря не знайдено.");
                    break;
                }

                Console.Write("Введіть годину (0-23): ");

                if (!int.TryParse(Console.ReadLine(), out int hour) ||
                    hour < 0 || hour > 23)
                {
                    Console.WriteLine("Некоректна година.");
                    break;
                }

                if (doctor.CanAcceptAt(hour))
                    Console.WriteLine("Лікар доступний у цю годину.");
                else
                    Console.WriteLine("Лікар не доступний у цю годину.");

                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}


// ==================== APPOINTMENT MENU ====================

static void AppointmentMenu(AppointmentManager manager)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("=== Записи ===");
        Console.WriteLine("1. Показати всі");
        Console.WriteLine("2. Створити запис");
        Console.WriteLine("3. Майбутні записи");
        Console.WriteLine("4. Скасувати");
        Console.WriteLine("5. Завершити");
        Console.WriteLine("0. Назад");
        Console.Write("Ваш вибір: ");

        string choice = Console.ReadLine()!;

        switch (choice)
        {
            case "1":
                manager.DisplayAll();
                break;

            case "2":
                Console.Write("ID пацієнта: ");

                if (!int.TryParse(Console.ReadLine(), out int patientId))
                {
                    Console.WriteLine("Некоректний ID пацієнта.");
                    break;
                }

                Console.Write("ID лікаря: ");

                if (!int.TryParse(Console.ReadLine(), out int doctorId))
                {
                    Console.WriteLine("Некоректний ID лікаря.");
                    break;
                }

                Console.Write("Дата (дд.мм.рррр): ");
                string dateText = Console.ReadLine()!;

                Console.Write("Година (0-23): ");

                if (!int.TryParse(Console.ReadLine(), out int hour) ||
                    hour < 0 || hour > 23)
                {
                    Console.WriteLine("Некоректна година.");
                    break;
                }

                if (!DateTime.TryParse(dateText, out DateTime date))
                {
                    Console.WriteLine("Некоректна дата.");
                    break;
                }

                DateTime appointmentDate = new DateTime(
                    date.Year,
                    date.Month,
                    date.Day,
                    hour,
                    0,
                    0
                );

                Console.Write("Причина: ");
                string reason = Console.ReadLine()!;

                manager.Schedule(
                    patientId,
                    doctorId,
                    appointmentDate,
                    reason
                );

                break;

            case "3":
                manager.DisplayUpcoming();
                break;

            case "4":
                Console.Write("ID запису: ");

                if (int.TryParse(Console.ReadLine(), out int cancelId) &&
                    manager.Cancel(cancelId))
                {
                    Console.WriteLine("Запис скасовано.");
                }
                else
                {
                    Console.WriteLine("Не вдалося скасувати запис.");
                }

                break;

            case "5":
                Console.Write("ID запису: ");

                if (int.TryParse(Console.ReadLine(), out int completeId) &&
                    manager.Complete(completeId))
                {
                    Console.WriteLine("Запис завершено.");
                }
                else
                {
                    Console.WriteLine("Не вдалося завершити запис.");
                }

                break;
            case "6":
                TestGrowableManager();
                break;
            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}
static void TestGrowableManager()
{
    Console.WriteLine();
    Console.WriteLine("=== Тест зростаючого масиву ===");

    GrowablePatientManager manager =
        new GrowablePatientManager();

    Patient patient1 = new Patient("Анна", "Іваненко");
    Patient patient2 = new Patient("Олег", "Петренко");
    Patient patient3 = new Patient("Марія", "Коваль");
    Patient patient4 = new Patient("Іван", "Бойко");
    Patient patient5 = new Patient("Олена", "Мельник");

    manager.Add(patient1);
    manager.Add(patient2);
    manager.Add(patient3);
    manager.Add(patient4);
    manager.Add(patient5);

    manager.DisplayAll();
}