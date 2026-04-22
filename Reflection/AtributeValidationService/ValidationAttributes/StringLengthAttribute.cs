namespace ValidationAttributes
{
    public sealed class StringLengthAttribute : ValidationAttribute
    {
        public StringLengthAttribute(int minLength, int maxLength)
        {
            this.MaxLength = maxLength;
            this.MinLength = minLength;
        }

        public int MaxLength { get; }

        public int MinLength { get; }

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }

            if (value is string str)
            {
                return str.Length >= this.MinLength
                    && str.Length <= this.MaxLength;
            }

            return false;
        }
    }
}
