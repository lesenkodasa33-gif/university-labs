using ClinicApp;

Clinic clinic = new Clinic("Медична Клініка");

// ==================== ПАЦІЄНТИ ====================

clinic.Patients.Add(new Patient(
    "Іван",
    "Петренко",
    new DateTime(1985, 5, 10),
    "A+",
    "0501234567"));

clinic.Patients.Add(new Patient(
    "Олена",
    "Коваль",
    new DateTime(1992, 8, 15),
    "B-",
    "0672345678"));

clinic.Patients.Add(new Patient(
    "Максим",
    "Бойко",
    new DateTime(2010, 3, 20),
    "O+",
    "0933456789"));

clinic.Patients.Add(new Patient("Марія", "Ткач"));


// ==================== ЛІКАРІ ====================

Doctor doctor1 = new Doctor(
    "Олег",
    "Сидоренко",
    "Кардіологія",
    "LIC-001",
    "0441234567");

doctor1.WorkStartHour = 8;
doctor1.WorkEndHour = 16;


Doctor doctor2 = new Doctor(
    "Наталія",
    "Мороз",
    "Неврологія",
    "LIC-002",
    "0442345678");

doctor2.WorkStartHour = 9;
doctor2.WorkEndHour = 18;


Doctor doctor3 = new Doctor(
    "Андрій",
    "Власенко",
    "Педіатрія",
    "LIC-003",
    "0443456789");

doctor3.WorkStartHour = 8;
doctor3.WorkEndHour = 17;


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

    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            PatientMenu(clinic);
            break;

        case "2":
            DoctorMenu(clinic);
            break;

        case "3":
            AppointmentMenu(clinic);
            break;

        case "4":
            Console.Write("Дата (рррр-мм-дд): ");

            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                clinic.DisplaySchedule(date);
            }
            else
            {
                Console.WriteLine("Некоректна дата.");
            }

            break;

        case "5":
            clinic.GenerateReport();
            break;

        case "6":
            GrowablePatientTest();
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Невірний вибір.");
            break;
    }
}


// ==================== МЕНЮ ПАЦІЄНТІВ ====================

static void PatientMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("========== ПАЦІЄНТИ ==========");
        Console.WriteLine("1. Показати всіх");
        Console.WriteLine("2. Додати пацієнта");
        Console.WriteLine("3. Знайти за ім'ям");
        Console.WriteLine("4. Видалити");
        Console.WriteLine("5. Статистика");
        Console.WriteLine("0. Назад");
        Console.Write("Ваш вибір: ");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                clinic.Patients.DisplayAll();
                break;

            case "2":
                Console.Write("Ім'я: ");
                string firstName = Console.ReadLine() ?? "";

                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine() ?? "";

                Patient patient = new Patient(firstName, lastName);

                clinic.Patients.Add(patient);
                break;

            case "3":
                Console.Write("Введіть ім'я або прізвище: ");
                string name = Console.ReadLine() ?? "";

                Patient[] found = clinic.Patients.FindByName(name);

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
                Console.Write("ID пацієнта: ");

                if (int.TryParse(Console.ReadLine(), out int patientId))
                {
                    if (clinic.Patients.Remove(patientId))
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
                clinic.Patients.DisplayStats();
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}


// ==================== МЕНЮ ЛІКАРІВ ====================

static void DoctorMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("========== ЛІКАРІ ==========");
        Console.WriteLine("1. Показати всіх");
        Console.WriteLine("2. Додати лікаря");
        Console.WriteLine("3. Знайти за спеціальністю");
        Console.WriteLine("4. Знайти за ID");
        Console.WriteLine("5. Видалити");
        Console.WriteLine("6. Статистика");
        Console.WriteLine("0. Назад");
        Console.Write("Ваш вибір: ");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                clinic.Doctors.DisplayAll();
                break;

            case "2":
                Console.Write("Ім'я: ");
                string firstName = Console.ReadLine() ?? "";

                Console.Write("Прізвище: ");
                string lastName = Console.ReadLine() ?? "";

                Console.Write("Спеціальність: ");
                string speciality = Console.ReadLine() ?? "";

                Console.Write("Номер ліцензії: ");
                string license = Console.ReadLine() ?? "";

                Console.Write("Телефон: ");
                string phone = Console.ReadLine() ?? "";

                Doctor doctor = new Doctor(
                    firstName,
                    lastName,
                    speciality,
                    license,
                    phone);

                Console.Write("Початок роботи (0-23): ");

                if (int.TryParse(Console.ReadLine(), out int startHour) &&
                    startHour >= 0 &&
                    startHour <= 23)
                {
                    doctor.WorkStartHour = startHour;
                }

                Console.Write("Кінець роботи (0-23): ");

                if (int.TryParse(Console.ReadLine(), out int endHour) &&
                    endHour >= 0 &&
                    endHour <= 23)
                {
                    doctor.WorkEndHour = endHour;
                }

                clinic.Doctors.Add(doctor);
                break;

            case "3":
                Console.Write("Спеціальність: ");
                string searchSpeciality = Console.ReadLine() ?? "";

                Doctor[] found =
                    clinic.Doctors.FindBySpeciality(searchSpeciality);

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

            case "4":
                Console.Write("ID лікаря: ");

                if (int.TryParse(Console.ReadLine(), out int doctorId))
                {
                    Doctor? foundDoctor =
                        clinic.Doctors.FindById(doctorId);

                    if (foundDoctor == null)
                    {
                        Console.WriteLine("Лікаря не знайдено.");
                    }
                    else
                    {
                        Console.WriteLine(foundDoctor);
                    }
                }

                break;

            case "5":
                Console.Write("ID лікаря: ");

                if (int.TryParse(Console.ReadLine(), out int removeId))
                {
                    if (clinic.Doctors.Remove(removeId))
                    {
                        Console.WriteLine("Лікаря видалено.");
                    }
                    else
                    {
                        Console.WriteLine("Лікаря не знайдено.");
                    }
                }

                break;

            case "6":
                clinic.Doctors.DisplayStats();
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}


// ==================== МЕНЮ ЗАПИСІВ ====================

static void AppointmentMenu(Clinic clinic)
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("========== ЗАПИСИ ==========");
        Console.WriteLine("1. Створити запис");
        Console.WriteLine("2. Показати майбутні");
        Console.WriteLine("3. Записи пацієнта");
        Console.WriteLine("4. Записи лікаря");
        Console.WriteLine("5. Записи за датою");
        Console.WriteLine("6. Скасувати запис");
        Console.WriteLine("7. Завершити запис");
        Console.WriteLine("0. Назад");
        Console.Write("Ваш вибір: ");

        string choice = Console.ReadLine() ?? "";

        switch (choice)
        {
            case "1":
                clinic.Patients.DisplayAll();

                Console.Write("ID пацієнта: ");
                if (!int.TryParse(Console.ReadLine(), out int patientId))
                {
                    Console.WriteLine("Некоректний ID.");
                    break;
                }

                clinic.Doctors.DisplayAll();

                Console.Write("ID лікаря: ");
                if (!int.TryParse(Console.ReadLine(), out int doctorId))
                {
                    Console.WriteLine("Некоректний ID.");
                    break;
                }

                Console.Write("Дата та час (рррр-мм-дд гг:хх): ");
                if (!DateTime.TryParse(
                    Console.ReadLine(),
                    out DateTime scheduledAt))
                {
                    Console.WriteLine("Некоректна дата.");
                    break;
                }

                Console.Write("Тривалість у хвилинах: ");
                if (!int.TryParse(
                    Console.ReadLine(),
                    out int duration))
                {
                    Console.WriteLine("Некоректна тривалість.");
                    break;
                }

                clinic.Appointments.Book(
                    patientId,
                    doctorId,
                    scheduledAt,
                    duration);

                break;

            case "2":
                Appointment[] upcoming =
                    clinic.Appointments.GetUpcoming();

                clinic.Appointments.DisplayList(upcoming);
                break;

            case "3":
                Console.Write("ID пацієнта: ");

                if (int.TryParse(
                    Console.ReadLine(),
                    out int searchPatientId))
                {
                    Appointment[] patientAppointments =
                        clinic.Appointments.GetByPatient(
                            searchPatientId);

                    clinic.Appointments.DisplayList(
                        patientAppointments);
                }

                break;

            case "4":
                Console.Write("ID лікаря: ");

                if (int.TryParse(
                    Console.ReadLine(),
                    out int searchDoctorId))
                {
                    Appointment[] doctorAppointments =
                        clinic.Appointments.GetByDoctor(
                            searchDoctorId);

                    clinic.Appointments.DisplayList(
                        doctorAppointments);
                }

                break;

            case "5":
                Console.Write("Дата (рррр-мм-дд): ");

                if (DateTime.TryParse(
                    Console.ReadLine(),
                    out DateTime searchDate))
                {
                    Appointment[] dateAppointments =
                        clinic.Appointments.GetByDate(
                            searchDate);

                    clinic.Appointments.DisplayList(
                        dateAppointments);
                }

                break;

            case "6":
                Console.Write("ID запису: ");

                if (!int.TryParse(
                    Console.ReadLine(),
                    out int cancelId))
                {
                    Console.WriteLine("Некоректний ID.");
                    break;
                }

                Console.Write("Причина скасування: ");
                string reason = Console.ReadLine() ?? "";

                clinic.Appointments.Cancel(
                    cancelId,
                    reason);

                break;

            case "7":
                Console.Write("ID запису: ");

                if (int.TryParse(
                    Console.ReadLine(),
                    out int completeId))
                {
                    clinic.Appointments.Complete(
                        completeId);
                }

                break;

            case "0":
                return;

            default:
                Console.WriteLine("Невірний вибір.");
                break;
        }
    }
}


// ==================== TASK 08 ====================

static void GrowablePatientTest()
{
    GrowablePatientManager manager =
        new GrowablePatientManager();

    Console.WriteLine();
    Console.WriteLine("=== Тест зростаючого масиву ===");

    for (int i = 1; i <= 20; i++)
    {
        Patient patient = new Patient(
            "Тест",
            "Пацієнт" + i,
            new DateTime(2000, 1, 1),
            "O+",
            "0000000000");

        manager.Add(patient);
    }

    Console.WriteLine();
    Console.WriteLine("Кількість: " + manager.Count);
    Console.WriteLine("Ємність: " + manager.Capacity);

    Patient? found = manager.FindById(10);

    if (found != null)
    {
        Console.WriteLine("Знайдено: " + found);
    }
}