
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class AnalyticsManager
{
    private readonly AppointmentManager _appointments;
    private readonly DoctorManager _doctors;
    private readonly PatientManager _patients;

    public AnalyticsManager(
        AppointmentManager appointments,
        DoctorManager doctors,
        PatientManager patients)
    {
        _appointments = appointments;
        _doctors = doctors;
        _patients = patients;
    }

    public IEnumerable<DoctorStats> ComputeDoctorStats()
    {
        Appointment[] appointments = _appointments.GetAll();
        Doctor[] doctors = _doctors.GetAll();

        foreach (Doctor doctor in doctors)
        {
            int appointmentCount = 0;
            decimal totalRevenue = 0;
            DateTime lastAppointmentDate = DateTime.MinValue;

            foreach (Appointment appointment in appointments)
            {
                if (appointment.DoctorId == doctor.Id)
                {
                    appointmentCount++;
                    totalRevenue += appointment.GetCost();

                    if (appointment.ScheduledAt > lastAppointmentDate)
                    {
                        lastAppointmentDate = appointment.ScheduledAt;
                    }
                }
            }

            yield return new DoctorStats(
                doctor.Id,
                doctor.FullName,
                appointmentCount,
                totalRevenue,
                lastAppointmentDate
            );
        }
    }

    public IEnumerable<PatientStats> ComputePatientStats()
    {
        Appointment[] appointments = _appointments.GetAll();

        for (int i = 0; i < _patients.Count; i++)
        {
            Patient? patient = _patients[i];

            if (patient == null)
                continue;

            int visitCount = 0;
            decimal totalSpent = 0;
            DateTime lastVisitDate = DateTime.MinValue;

            foreach (Appointment appointment in appointments)
            {
                if (appointment.PatientId == patient.Id)
                {
                    visitCount++;
                    totalSpent += appointment.GetCost();

                    if (appointment.ScheduledAt > lastVisitDate)
                    {
                        lastVisitDate = appointment.ScheduledAt;
                    }
                }
            }

            yield return new PatientStats(
                patient.Id,
                patient.FullName,
                visitCount,
                totalSpent,
                lastVisitDate
            );
        }
    }
}
