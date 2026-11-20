
using System.Collections.Generic;

namespace ClinicApp.Utils
{
    public class ValidationResult
    {
        private readonly List<string> _errors = new();

        public IReadOnlyList<string> Errors => _errors;

        public bool IsValid => _errors.Count == 0;

        public void AddError(string error)
        {
            _errors.Add(error);
        }
    }
}
