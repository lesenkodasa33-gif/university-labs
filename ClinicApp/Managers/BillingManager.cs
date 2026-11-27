
using ClinicApp.Interfaces;
using ClinicApp.Events;

namespace ClinicApp.Managers;

public class BillingManager
{
    private readonly AppointmentManager _appointments;

    public event EventHandler<PaymentEventArgs>? PaymentReceived;

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

    public bool PayAppointment(int appointmentId)
    {
        var appointment = _appointments.FindById(appointmentId);

        if (appointment == null ||
            appointment.IsPaid ||
            appointment.IsCancelled)
        {
            return false;
        }

        decimal amount = appointment.GetCost();

        appointment.MarkPaid();

        PaymentReceived?.Invoke(
            this,
            new PaymentEventArgs(appointmentId, amount));

        return true;
    }
}
