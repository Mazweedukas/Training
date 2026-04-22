namespace ValidationAttributes
{
    public sealed class RequiredAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            return value != null;
        }
    }
}
