namespace ContiguousSubstrings
{
    /// <summary>
    /// Provides methods for working with sequences of digits and their substrings.
    /// </summary>
    public static class Sequences
    {
        /// <summary>
        /// Extracts all contiguous substrings of specified length from a string of digits using a sliding window approach.
        /// </summary>
        /// <param name="numbers">Source string containing only digits (0-9).</param>
        /// <param name="length">Length of substrings to extract. Must be positive and not exceed the input string length.</param>
        /// <returns>Collection of digit substrings, each of specified length, in their original order.</returns>
        /// <exception cref="ArgumentException">The <paramref name="length"/> parameter is less than or equal to zero or greater than the length of the input string.</exception>
        /// <exception cref="ArgumentException">The <paramref name="numbers"/> string contains non-digit characters.</exception>
        /// <exception cref="ArgumentException">The <paramref name="numbers"/> string is null, empty, or consists only of whitespace.</exception>
        /// <example>
        /// <code>
        /// // Basic usage
        /// var result1 = Sequences.GetSubstrings("12", 1); // ["1", "2"]
        /// var result2 = Sequences.GetSubstrings("9142", 2); // ["91", "14", "42"]
        ///
        /// // Edge cases
        /// var result3 = Sequences.GetSubstrings("777777", 3); // ["777", "777", "777", "777"]
        /// var result4 = Sequences.GetSubstrings("1", 1); // ["1"]
        ///
        /// // Error cases
        /// var result5 = Sequences.GetSubstrings("123a", 1); // ArgumentException
        /// var result6 = Sequences.GetSubstrings("12345", 6); // ArgumentException
        /// var result7 = Sequences.GetSubstrings("", 1); // ArgumentException
        /// </code>
        /// </example>
        /// <remarks>
        /// For input string of length n and substring length k, returns n - k + 1 substrings.
        /// </remarks>
        public static IEnumerable<string> GetSubstrings(string numbers, int length)
        {
            if (string.IsNullOrWhiteSpace(numbers))
            {
                throw new ArgumentException("The numbers argument is null, empty or whitespace", nameof(numbers));
            }

            if (length <= 0)
            {
                throw new ArgumentException("The length argument must be greater than zero", nameof(length));
            }

            if (numbers.Length < length)
            {
                throw new ArgumentException("The length argument must not exceed the length of the input string", nameof(length));
            }

            foreach (var number in numbers)
            {
                if (!char.IsDigit(number))
                {
                    throw new ArgumentException("The numbers argument must contain only digits (0-9)", nameof(numbers));
                }
            }

            var numberOfSubstrings = numbers.Length - length + 1;

            var results = new List<string>(numberOfSubstrings);

            for (var i = 0; i < numberOfSubstrings; i++)
            {
                results.Add(numbers.Substring(i, length));
            }

            return results;
        }
    }
}
