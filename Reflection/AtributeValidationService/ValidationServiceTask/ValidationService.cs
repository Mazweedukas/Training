using System.Reflection;
using ValidationAttributes;

namespace ValidationServiceTask
{
    public class ValidationService<T>
    {
        private readonly Dictionary<string, List<ValidationResult>> validationInfo =
            new Dictionary<string, List<ValidationResult>>();

        private object obj;
        private Type type;

        public IReadOnlyDictionary<string, List<ValidationResult>> ValidationInfo => this.validationInfo;

        public bool IsValid(T obj)
        {
            ArgumentNullException.ThrowIfNull(obj);

            this.obj = obj;
            this.type = obj.GetType();

            this.validationInfo.Clear();

            this.ValidateMembers();
            return this.validationInfo.Count == 0;
        }

        private void ValidateMembers()
        {
            foreach (var property in this.type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                this.ValidateMember(
                               property.Name,
                               property.GetValue(this.obj),
                               property.GetCustomAttributes<ValidationAttribute>());
            }

            foreach (var field in this.type.GetFields(BindingFlags.Instance | BindingFlags.Public))
            {
                this.ValidateMember(
                               field.Name,
                               field.GetValue(this.obj),
                               field.GetCustomAttributes<ValidationAttribute>());
            }
        }

        private void ValidateMember(string name, object? value, IEnumerable<ValidationAttribute> attributes)
        {
            List<ValidationResult> results = new ();

            foreach (var attribute in attributes)
            {
                if (!attribute.IsValid(value))
                {
                    string message;

                    if (!string.IsNullOrWhiteSpace(attribute.ErrorMessage))
                    {
                        message = attribute.ErrorMessage;
                    }
                    else if (attribute is NumericRangeAttribute)
                    {
                        message = $"The field must be between {attribute.GetType().GetProperty("Minimum")} and {attribute.GetType().GetProperty("Maximum")}.";
                    }
                    else
                    {
                        message = attribute.ErrorMessage;
                    }

                    results.Add(new ValidationResult(attribute.GetType(), message));
                }
            }

            if (results.Count > 0)
            {
                this.validationInfo[name] = results;
            }
        }
    }
}
