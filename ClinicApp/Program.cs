using ClinicApp;
using ClinicApp.Enums;
using ClinicApp.Managers;
using ClinicApp.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.InputEncoding = System.Text.Encoding.UTF8;
Clinic clinic = new Clinic("Медична Клініка");

// ==================== ПАЦІЄНТИ ====================

clinic.Patients.Add(new Patient(
    "Іван",
    "Петренко",
    new DateTime(1985, 5, 10),
    BloodType.APositive,
    "0501234567"));

clinic.Patients.Add(new Patient(
    "Олена",
    "Коваль",
    new DateTime(1992, 8, 15),
    BloodType.BNegative,
    "0672345678"));

clinic.Patients.Add(new Patient(
    "Максим",
    "Бойко",
    new DateTime(2010, 3, 20),
    BloodType.OPositive,
    "0933456789"));

clinic.Patients.Add(new Patient("Марія", "Ткач"));


// ==================== ЛІКАРІ ====================

Doctor doctor1 = new Doctor(
    "Олег",
    "Сидоренко",
    Speciality.Cardiology,
    "LIC-001",
    "0441234567");

doctor1.Schedule = new WorkSchedule(8, 16);


Doctor doctor2 = new Doctor(
    "Наталія",
    "Мороз",
    Speciality.Neurology,
    "LIC-002",
    "0442345678");

doctor2.Schedule = new WorkSchedule(9, 18);

Doctor doctor3 = new Doctor(
    "Андрій",
    "Власенко",
    Speciality.Pediatrics,
    "LIC-003",
    "0443456789");

doctor3.Schedule = new WorkSchedule(8, 17);


clinic.Doctors.Add(doctor1);
clinic.Doctors.Add(doctor2);
clinic.Doctors.Add(doctor3);

clinic.MedicalRecords.Add(new Diagnosis(
    1, 1, DateTime.Today.AddDays(-30),
    "I10", "Гіпертонічна хвороба", true));

clinic.MedicalRecords.Add(new Diagnosis(
    1, 2, DateTime.Today.AddDays(-10),
    "J06", "Гостра респіраторна інфекція", false));

clinic.MedicalRecords.Add(new LabResult(
    1, 1, DateTime.Today.AddDays(-5),
    "Гемоглобін", 135, "г/л", "120-150", true));

clinic.MedicalRecords.Add(new Prescription(
    1, 1, DateTime.Today.AddDays(-3),
    "Лізиноприл", "10 мг", 30,
    "1 раз на добу вранці"));

clinic.MedicalRecords.Add(new Prescription(
    2, 2, DateTime.Today.AddDays(-40),
    "Амоксицилін", "500 мг", 7,
    "3 рази на добу"));





// ==================== ГОЛОВНЕ МЕНЮ ====================

while (true)
{
    Console.WriteLine();
    Console.WriteLine("========== КЛІНІКА ==========");
    Console.WriteLine("1. Пацієнти");
    Console.WriteLine("2. Лікарі");
    Console.WriteLine("3. Записи");
    Console.WriteLine("4. Медична картка");
    Console.WriteLine("5. Розклад");
    Console.WriteLine("6. Звіт");
    Console.WriteLine("7. Зростаючий масив");
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
            MedicalRecordMenu(clinic);
            break;

        case "5":
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

        case "6":
            clinic.GenerateReport();
            break;

        case "7":
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
    Console.WriteLine("Демонстрація копіювання структури:");

    WorkSchedule morning = new WorkSchedule(8, 16);
    WorkSchedule copy = morning;

    copy = new WorkSchedule(10, 18);

    Console.WriteLine("Оригінал: " + morning);
    Console.WriteLine("Копія: " + copy);

 

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
                try
                {
                    Console.Write("Ім'я: ");
                    string firstName = Console.ReadLine() ?? "";

                    Console.Write("Прізвище: ");
                    string lastName = Console.ReadLine() ?? "";

                    Patient patient = new Patient(firstName, lastName);

                    clinic.Patients.Add(patient);

                    Console.WriteLine("Пацієнта успішно додано.");
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Помилка: " + e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Помилка: " + e.Message);
                }

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
                try
                {
                    Console.Write("Ім'я: ");
                    string firstName = Console.ReadLine() ?? "";

                    Console.Write("Прізвище: ");
                    string lastName = Console.ReadLine() ?? "";

                    Console.WriteLine("Доступні спеціальності:");
                    Console.WriteLine(string.Join(", ", Enum.GetNames<Speciality>()));

                    Console.Write("Спеціальність: ");
                    string specialityInput = Console.ReadLine() ?? "";

                    if (!Enum.TryParse<Speciality>(
                            specialityInput,
                            true,
                            out Speciality speciality) ||
                        !Enum.IsDefined(speciality))
                    {
                        Console.WriteLine("Некоректна спеціальність.");
                        break;
                    }

                    Console.Write("Номер ліцензії: ");
                    string license = Console.ReadLine() ?? "";

                    Console.Write("Телефон: ");
                    string phone = Console.ReadLine() ?? "";

                    Console.Write("Початок роботи (0-23): ");
                    if (!int.TryParse(Console.ReadLine(), out int startHour))
                    {
                        Console.WriteLine("Некоректний час.");
                        break;
                    }

                    Console.Write("Кінець роботи (1-24): ");
                    if (!int.TryParse(Console.ReadLine(), out int endHour))
                    {
                        Console.WriteLine("Некоректний час.");
                        break;
                    }

                    Doctor doctor = new Doctor(
                        firstName,
                        lastName,
                        speciality,
                        license,
                        phone);

                    doctor.Schedule = new WorkSchedule(startHour, endHour);

                    clinic.Doctors.Add(doctor);

                    Console.WriteLine("Лікаря успішно додано.");
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Помилка: " + e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Помилка: " + e.Message);
                }

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

                    Doctor? foundDoctor = clinic.Doctors.FindById(doctorId);

                    Console.WriteLine(
                        foundDoctor?.ToString() ?? "Лікаря не знайдено.");

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

                try
                {
                    clinic.Appointments.Book(
                        patientId,
                        doctorId,
                        scheduledAt,
                        duration);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Помилка: " + e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Помилка: " + e.Message);
                }

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
            BloodType.OPositive,
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
    static void MedicalRecordMenu(Clinic clinic)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("===== МЕДИЧНА КАРТКА =====");
            Console.WriteLine("1. Зведення по пацієнту");
            Console.WriteLine("2. Усі записи пацієнта");
            Console.WriteLine("3. Додати діагноз");
            Console.WriteLine("4. Додати аналіз");
            Console.WriteLine("5. Додати рецепт");
            Console.WriteLine("6. Записи лікаря");
            Console.WriteLine("0. Назад");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    Console.Write("ID пацієнта: ");
                    if (int.TryParse(Console.ReadLine(), out int patientId))
                        clinic.MedicalRecords.DisplayPatientSummary(patientId);
                    else
                        Console.WriteLine("Некоректний ID.");
                    break;

                case "2":
                    Console.Write("ID пацієнта: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                        clinic.MedicalRecords.DisplayList(
                            clinic.MedicalRecords.GetByPatient(id));
                    else
                        Console.WriteLine("Некоректний ID.");
                    break;

            case "3":
                try
                {
                    Console.Write("ID пацієнта: ");
                    if (!int.TryParse(Console.ReadLine(), out int diagnosisPatientId)
                        || diagnosisPatientId <= 0)
                    {
                        Console.WriteLine("Некоректний ID пацієнта.");
                        break;
                    }

                    Console.Write("ID лікаря: ");
                    if (!int.TryParse(Console.ReadLine(), out int diagnosisDoctorId)
                        || diagnosisDoctorId <= 0)
                    {
                        Console.WriteLine("Некоректний ID лікаря.");
                        break;
                    }

                    if (clinic.Patients.FindById(diagnosisPatientId) == null ||
                        clinic.Doctors.FindById(diagnosisDoctorId) == null)
                    {
                        Console.WriteLine("Пацієнта або лікаря не знайдено.");
                        break;
                    }

                    Console.Write("Код діагнозу: ");
                    string diagnosisCode = Console.ReadLine() ?? "";

                    Console.Write("Опис діагнозу: ");
                    string description = Console.ReadLine() ?? "";

                    Console.Write("Хронічний? (так/ні): ");
                    bool isChronic = (Console.ReadLine() ?? "")
                        .Trim().Equals("так", StringComparison.OrdinalIgnoreCase);

                    Diagnosis diagnosis = new Diagnosis(
                        diagnosisPatientId,
                        diagnosisDoctorId,
                        DateTime.Today,
                        diagnosisCode,
                        description,
                        isChronic);

                    clinic.MedicalRecords.Add(diagnosis);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Помилка: " + ex.Message);
                }
                break;

            case "4":
                try
                {
                    Console.Write("ID пацієнта: ");
                    if (!int.TryParse(Console.ReadLine(), out int labPatientId)
                        || labPatientId <= 0)
                    {
                        Console.WriteLine("Некоректний ID пацієнта.");
                        break;
                    }

                    Console.Write("ID лікаря: ");
                    if (!int.TryParse(Console.ReadLine(), out int labDoctorId)
                        || labDoctorId <= 0)
                    {
                        Console.WriteLine("Некоректний ID лікаря.");
                        break;
                    }

                    if (clinic.Patients.FindById(labPatientId) == null ||
                        clinic.Doctors.FindById(labDoctorId) == null)
                    {
                        Console.WriteLine("Пацієнта або лікаря не знайдено.");
                        break;
                    }

                    Console.Write("Назва аналізу: ");
                    string testName = Console.ReadLine() ?? "";

                    Console.Write("Значення: ");
                    if (!double.TryParse(
                        Console.ReadLine(),
                        System.Globalization.NumberStyles.Float,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out double value) ||
                        !double.IsFinite(value))
                    {
                        Console.WriteLine("Некоректне значення. Використовуй крапку.");
                        break;
                    }

                    Console.Write("Одиниця вимірювання: ");
                    string unit = Console.ReadLine() ?? "";

                    Console.Write("Референсний діапазон: ");
                    string referenceRange = Console.ReadLine() ?? "";

                    Console.Write("У межах норми? (так/ні): ");
                    string normalInput = (Console.ReadLine() ?? "").Trim();

                    if (normalInput != "так" && normalInput != "ні")
                    {
                        Console.WriteLine("Введи 'так' або 'ні'.");
                        break;
                    }

                    bool isNormal = normalInput == "так";

                    LabResult labResult = new LabResult(
                        labPatientId,
                        labDoctorId,
                        DateTime.Today,
                        testName,
                        value,
                        unit,
                        referenceRange,
                        isNormal);

                    clinic.MedicalRecords.Add(labResult);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Помилка: " + ex.Message);
                }
                break;


            case "5":
                try
                {
                    Console.Write("ID пацієнта: ");
                    if (!int.TryParse(Console.ReadLine(), out int prescriptionPatientId)
                        || prescriptionPatientId <= 0)
                    {
                        Console.WriteLine("Некоректний ID пацієнта.");
                        break;
                    }

                    Console.Write("ID лікаря: ");
                    if (!int.TryParse(Console.ReadLine(), out int prescriptionDoctorId)
                        || prescriptionDoctorId <= 0)
                    {
                        Console.WriteLine("Некоректний ID лікаря.");
                        break;
                    }

                    if (clinic.Patients.FindById(prescriptionPatientId) == null ||
                        clinic.Doctors.FindById(prescriptionDoctorId) == null)
                    {
                        Console.WriteLine("Пацієнта або лікаря не знайдено.");
                        break;
                    }

                    Console.Write("Назва препарату: ");
                    string medicationName = Console.ReadLine() ?? "";

                    Console.Write("Дозування: ");
                    string dosage = Console.ReadLine() ?? "";

                    Console.Write("Тривалість лікування (днів): ");
                    if (!int.TryParse(Console.ReadLine(), out int durationDays)
                        || durationDays <= 0)
                    {
                        Console.WriteLine("Некоректна тривалість.");
                        break;
                    }

                    Console.Write("Інструкція: ");
                    string instructions = Console.ReadLine() ?? "";

                    Prescription prescription = new Prescription(
                        prescriptionPatientId,
                        prescriptionDoctorId,
                        DateTime.Today,
                        medicationName,
                        dosage,
                        durationDays,
                        instructions);

                    clinic.MedicalRecords.Add(prescription);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Помилка: " + ex.Message);
                }
                break;

            case "6":
                Console.Write("ID лікаря: ");

                if (!int.TryParse(Console.ReadLine(), out int recordDoctorId)
                    || recordDoctorId <= 0)
                {
                    Console.WriteLine("Некоректний ID лікаря.");
                    break;
                }

                Doctor? recordDoctor = clinic.Doctors.FindById(recordDoctorId);

                if (recordDoctor == null)
                {
                    Console.WriteLine("Лікаря не знайдено.");
                    break;
                }

                Console.WriteLine();
                Console.WriteLine($"Медичні записи лікаря: {recordDoctor.FullName}");

                MedicalRecord[] doctorRecords =
                    clinic.MedicalRecords.GetByDoctor(recordDoctorId);

                clinic.MedicalRecords.DisplayList(doctorRecords);

                break;

            case "0":
                    return;

                default:
                    Console.WriteLine("Цей пункт поки не реалізований.");
                    break;
            }
        }
    }

