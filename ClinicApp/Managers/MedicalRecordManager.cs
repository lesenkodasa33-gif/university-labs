
using System;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class MedicalRecordManager
{
    private const int MaxRecords = 1000;
    private MedicalRecord[] _records = new MedicalRecord[MaxRecords];
    private int _count = 0;

    public int Count => _count;

    public void Add(MedicalRecord record)
    {
        if (_count >= MaxRecords)
        {
            Console.WriteLine("Досягнуто ліміту записів.");
            return;
        }

        _records[_count] = record;
        _count++;

        Console.WriteLine(
            $"Запис [{record.Id}] {record.GetRecordType()} додано.");
    }

    public MedicalRecord? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_records[i].Id == id)
                return _records[i];
        }

        return null;
    }

    public MedicalRecord[] GetByPatient(int patientId)
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId)
                count++;
        }

        MedicalRecord[] result = new MedicalRecord[count];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId)
            {
                result[index] = _records[i];
                index++;
            }
        }

        return result;
    }

    public MedicalRecord[] GetByDoctor(int doctorId)
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].DoctorId == doctorId)
                count++;
        }

        MedicalRecord[] result = new MedicalRecord[count];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].DoctorId == doctorId)
            {
                result[index] = _records[i];
                index++;
            }
        }

        return result;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Медичних записів немає.");
            return;
        }

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(_records[i]);
        }
    }

    public void DisplayList(MedicalRecord[] records)
    {
        if (records.Length == 0)
        {
            Console.WriteLine("Записів не знайдено.");
            return;
        }

        for (int i = 0; i < records.Length; i++)
        {
            Console.WriteLine(records[i]);
        }
    }

    public MedicalRecord? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
                return null;

            return _records[index];
        }
    }

    public Diagnosis[] GetDiagnoses(int patientId)
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Diagnosis)
            {
                count++;
            }
        }

        Diagnosis[] result = new Diagnosis[count];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Diagnosis diagnosis)
            {
                result[index] = diagnosis;
                index++;
            }
        }

        return result;
    }

    public LabResult[] GetLabResults(int patientId)
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is LabResult)
            {
                count++;
            }
        }

        LabResult[] result = new LabResult[count];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is LabResult labResult)
            {
                result[index] = labResult;
                index++;
            }
        }

        return result;
    }

    public Prescription[] GetPrescriptions(int patientId)
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Prescription)
            {
                count++;
            }
        }

        Prescription[] result = new Prescription[count];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Prescription prescription)
            {
                result[index] = prescription;
                index++;
            }
        }

        return result;
    }

    public Diagnosis[] GetChronicDiagnoses(int patientId)
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Diagnosis diagnosis &&
                diagnosis.IsChronic)
            {
                count++;
            }
        }

        Diagnosis[] result = new Diagnosis[count];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            Diagnosis? diagnosis = _records[i] as Diagnosis;

            if (diagnosis != null &&
                diagnosis.PatientId == patientId &&
                diagnosis.IsChronic)
            {
                result[index] = diagnosis;
                index++;
            }
        }

        return result;
    }

    public Prescription[] GetActivePrescriptions(int patientId)
    {
        int count = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_records[i].PatientId == patientId &&
                _records[i] is Prescription prescription &&
                prescription.IsActive())
            {
                count++;
            }
        }

        Prescription[] result = new Prescription[count];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            Prescription? prescription = _records[i] as Prescription;

            if (prescription != null &&
                prescription.PatientId == patientId &&
                prescription.IsActive())
            {
                result[index] = prescription;
                index++;
            }
        }

        return result;
    }

    public void DisplayPatientSummary(int patientId)
    {
        Diagnosis[] diagnoses = GetDiagnoses(patientId);
        LabResult[] labResults = GetLabResults(patientId);
        Prescription[] prescriptions = GetPrescriptions(patientId);

        Diagnosis[] chronic = GetChronicDiagnoses(patientId);
        Prescription[] active = GetActivePrescriptions(patientId);

        Console.WriteLine();
        Console.WriteLine($"=== Медична картка пацієнта #{patientId} ===");

        Console.WriteLine($"Діагнозів: {diagnoses.Length}");
        Console.WriteLine($"Аналізів: {labResults.Length}");
        Console.WriteLine($"Рецептів: {prescriptions.Length}");

        Console.WriteLine();
        Console.WriteLine($"Хронічних діагнозів: {chronic.Length}");

        for (int i = 0; i < chronic.Length; i++)
        {
            Console.WriteLine(chronic[i]);
        }

        Console.WriteLine();
        Console.WriteLine($"Активних рецептів: {active.Length}");

        for (int i = 0; i < active.Length; i++)
        {
            Console.WriteLine(active[i]);
        }

        Console.WriteLine("==============================");
    }

}
