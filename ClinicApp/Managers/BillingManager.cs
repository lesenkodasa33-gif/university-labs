
using ClinicApp.Interfaces;

namespace ClinicApp.Managers;

public class BillingManager
{
    private readonly AppointmentManager _appointments;

    public BillingManager(AppointmentManager appointments)
    {
        _appointments = appointments;
    }

    public IPayable[] GetUnpaid()
    {
        var all = _appointments.GetAll();

        int count = 0;

        foreach (var appointment in all)
        {
            if (!appointment.IsPaid && !appointment.IsCancelled)
            {
                count++;
            }
        }

        IPayable[] result = new IPayable[count];
        int index = 0;

        foreach (var appointment in all)
        {
            if (!appointment.IsPaid && !appointment.IsCancelled)
            {
                result[index] = appointment;
                index++;
            }
        }

        return result;
    }

    public decimal GetTotalDebt()
    {
        decimal total = 0;

        foreach (IPayable item in GetUnpaid())
        {
            total += item.GetCost();
        }

        return total;
    }
}
