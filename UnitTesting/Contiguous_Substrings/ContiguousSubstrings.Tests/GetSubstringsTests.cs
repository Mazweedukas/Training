using NUnit.Framework;

namespace ContiguousSubstrings.Tests
{
    [TestFixture]
    public sealed class GetSubstringsTests
    {
        private static readonly string[] TestResult12345And2 = { "12", "23", "34", "45" };
        private static readonly string[] TestResult12345And3 = { "123", "234", "345" };
        private static readonly string[] TestResult777777And3 = { "777", "777", "777", "777" };

        private static readonly object[] BasicUsageCases =
        {
            new object[] { "12345", 2, TestResult12345And2 },
            new object[] { "12345", 3, TestResult12345And3 },
            new object[] { "777777", 3, TestResult777777And3 },
        };

        [TestCase("  ", 1)]
        [TestCase(null, 1)]
        [TestCase("", 2)]
        public void GetSubstrings_ArgumentNotNullOrEmpty(string numbers, int length)
        {
            Assert.That(() => Sequences.GetSubstrings(numbers, length), Throws.ArgumentException);
        }

        [TestCase("12345", 0)]
        [TestCase("12345", 6)]
        public void GetSubstrings_ArgumentLengthOutOfRange(string numbers, int length)
        {
            Assert.That(() => Sequences.GetSubstrings(numbers, length), Throws.ArgumentException);
        }

        [TestCase("123a", 1)]
        [TestCase("12b45", 3)]
        public void GetSubstrings_ArgumentNumbersNotDigits(string numbers, int length)
        {
            Assert.That(() => Sequences.GetSubstrings(numbers, length), Throws.ArgumentException);
        }

        [TestCaseSource(nameof(BasicUsageCases))]
        public void GetSubstrings_BasicUsage(string numbers, int length, string[] results)
        {
            var result = Sequences.GetSubstrings(numbers, length);
            Assert.That(result, Is.EqualTo(results));
        }
    }
}
