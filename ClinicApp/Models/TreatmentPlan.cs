namespace ClinicApp.Models;

public class TreatmentPlan
{
    public int Id { get; }
    public int PatientId { get; }
    public string Description { get; }
    public bool IsCompleted { get; private set; }

    public TreatmentPlan(int id, int patientId, string description)
    {
        Id = id;
        PatientId = patientId;
        Description = description;
    }

    public void Complete()
    {
        IsCompleted = true;
    }
}