using System.Globalization;

namespace ValidationAttributes
{
    public sealed class NumericRangeAttribute : ValidationAttribute
    {
        public NumericRangeAttribute(double minimum, double maximum)
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
        }

        public NumericRangeAttribute(object minimum, object maximum, Type numericType)
        {
            this.Minimum = Convert.ToDouble(minimum, CultureInfo.InvariantCulture);
            this.Maximum = Convert.ToDouble(maximum, CultureInfo.InvariantCulture);
            this.NumericType = numericType;
        }

        private Type? NumericType { get; }

        private double Minimum { get; }

        private double Maximum { get; }

        public override bool IsValid(object? value)
        {
            if (this.Minimum >= this.Maximum)
            {
                throw new ArgumentException("Minimum can't be greater than maximum");
            }

            if (value == null)
            {
                return true;
            }

            if (value is not(byte
                         or sbyte
                         or short
                         or ushort
                         or int
                         or uint
                         or long
                         or ulong
                         or float
                         or double
                         or decimal))
            {
                return false;
            }

            try
            {
                var result = Convert.ToDouble(value, CultureInfo.InvariantCulture);
                return result >= this.Minimum
                    && result <= this.Maximum;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
