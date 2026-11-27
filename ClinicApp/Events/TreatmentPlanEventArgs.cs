namespace ClinicApp.Events;

public class TreatmentPlanEventArgs : EventArgs
{
    public int PlanId { get; }

    public TreatmentPlanEventArgs(int planId)
    {
        PlanId = planId;
    }
}