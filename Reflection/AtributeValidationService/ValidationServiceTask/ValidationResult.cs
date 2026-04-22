namespace ValidationServiceTask
{
    public sealed class ValidationResult
    {
        public ValidationResult(Type attributeType, string? validationMessage)
        {
            this.AttributeType = attributeType;
            this.ValidationMessage = validationMessage;
        }

        public Type AttributeType { get; init; }

        public string? ValidationMessage { get; init; }
    }
}
