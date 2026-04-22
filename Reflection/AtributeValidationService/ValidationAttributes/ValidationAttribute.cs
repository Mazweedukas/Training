namespace ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public abstract class ValidationAttribute : Attribute
    {
        protected ValidationAttribute()
        {
            this.ErrorMessage = "The field is invalid.";
        }

        protected ValidationAttribute(string errorMessage)
        {
            this.ErrorMessage = errorMessage ?? "The field is invalid.";
        }

        public string ErrorMessage { get; set; }

        public abstract bool IsValid(object? value);
    }
}
