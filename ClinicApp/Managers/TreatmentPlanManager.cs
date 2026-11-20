
using System.Collections.Generic;
using ClinicApp.Models;
using ClinicApp.Utils;

namespace ClinicApp.Managers
{
    public class TreatmentPlanManager
    {
        private readonly List<TreatmentPlan> _plans = new();

        public int Count => _plans.Count;

        public bool Add(TreatmentPlan plan)
        {
            if (plan == null)
            {
                Console.WriteLine("План лікування не може бути null.");
                return false;
            }

            ValidationResult result = ModelValidator.Validate(plan);

            if (!result.IsValid)
            {
                Console.WriteLine("Помилки валідації:");

                foreach (string error in result.Errors)
                {
                    Console.WriteLine($"- {error}");
                }

                Console.WriteLine("План не додано.");
                return false;
            }

            _plans.Add(plan);

            Console.WriteLine(
                $"План лікування [{plan.Id}] успішно додано.");

            return true;
        }

        public TreatmentPlan? FindById(int id)
        {
            foreach (TreatmentPlan plan in _plans)
            {
                if (plan.Id == id)
                {
                    return plan;
                }
            }

            return null;
        }

        public TreatmentPlan[] GetAll()
        {
            return _plans.ToArray();
        }

        public void DisplayAll()
        {
            if (_plans.Count == 0)
            {
                Console.WriteLine("Список планів лікування порожній.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"=== Плани лікування ({_plans.Count}) ===");

            foreach (TreatmentPlan plan in _plans)
            {
                Console.WriteLine(
                    $"ID: {plan.Id} | " +
                    $"Patient ID: {plan.PatientId} | " +
                    $"Diagnosis: {plan.Diagnosis} | " +
                    $"Duration: {plan.DurationDays} days | " +
                    $"Status: {plan.Status}");
            }

            Console.WriteLine(
                "--------------------------------------------------");
        }
    }
}
