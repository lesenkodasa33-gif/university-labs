using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Managers;

public class TreatmentPlanManager
{
    private readonly List<TreatmentPlan> _plans = new();

    public event EventHandler<TreatmentPlanEventArgs>? PlanCompleted;

    public void Add(TreatmentPlan plan)
    {
        _plans.Add(plan);
    }

    public bool Complete(int planId)
    {
        var plan = _plans.FirstOrDefault(p => p.Id == planId);

        if (plan == null || plan.IsCompleted)
            return false;

        plan.Complete();

        PlanCompleted?.Invoke(
            this,
            new TreatmentPlanEventArgs(planId));

        return true;
    }
}