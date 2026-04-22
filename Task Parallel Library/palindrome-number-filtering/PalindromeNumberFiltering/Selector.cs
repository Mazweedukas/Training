using System.Collections.Concurrent;

namespace PalindromeNumberFiltering;

/// <summary>
/// A static class containing methods for filtering palindrome numbers from a collection of integers.
/// </summary>
public static class Selector
{
    /// <summary>
    /// Retrieves a collection of palindrome numbers from the given list of integers using sequential filtering.
    /// </summary>
    /// <param name="numbers">The list of integers to filter.</param>
    /// <returns>A collection of palindrome numbers.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input list 'numbers' is null.</exception>
    public static IList<int> GetPalindromeInSequence(IList<int>? numbers)
    {
        ArgumentNullException.ThrowIfNull(numbers);

        List<int> palindromes = new();

        foreach (var number in numbers)
        {
            if (IsPalindrome(number))
            {
                palindromes.Add(number);
            }
        }

        return palindromes;
    }

    /// <summary>
    /// Retrieves a collection of palindrome numbers from the given list of integers using parallel filtering.
    /// </summary>
    /// <param name="numbers">The list of integers to filter.</param>
    /// <returns>A collection of palindrome numbers.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input list 'numbers' is null.</exception>
    public static IList<int> GetPalindromeInParallel(IList<int> numbers)
    {
        ConcurrentBag<int> palindromes = new();

        Parallel.ForEach(numbers, n =>
        {
            if (IsPalindrome(n))
            {
                palindromes.Add(n);
            }
        });

        return palindromes.ToList();
    }

    /// <summary>
    /// Checks whether the given integer is a palindrome number.
    /// </summary>
    /// <param name="number">The integer to check.</param>
    /// <returns>True if the number is a palindrome, otherwise false.</returns>
    private static bool IsPalindrome(int number)
    {
        var divider = 1;

        for (int i = 1; i < GetLength(number); i++)
        {
            divider *= 10;
        }

        return IsPositiveNumberPalindrome(number, divider);
    }

    /// <summary>
    /// Recursively checks whether a positive number is a palindrome.
    /// </summary>
    /// <param name="number">The positive number to check.</param>
    /// <param name="divider">The divider used in the recursive check.</param>
    /// <returns>True if the positive number is a palindrome, otherwise false.</returns>
    private static bool IsPositiveNumberPalindrome(int number, int divider)
    {
        if (number < 0)
        {
            return false;
        }

        if (number < 10)
        {
            return true;
        }

        var firstNumber = number / divider;
        var lastNumber = number % 10;

        if (firstNumber != lastNumber)
        {
            return false;
        }

        int trimmed = (number % divider) / 10;

        return IsPositiveNumberPalindrome(trimmed, divider / 100);
    }

    /// <summary>
    /// Gets the number of digits in the given integer.
    /// </summary>
    /// <param name="number">The integer to count digits for.</param>
    /// <returns>The number of digits in the integer.</returns>
    private static byte GetLength(int number)
    {
        number = Math.Abs(number);

        switch (number)
        {
            case < 10: return 1;
            case < 100: return 2;
            case < 1_000: return 3;
            case < 10_000: return 4;
            case < 100_000: return 5;
            case < 1_000_000: return 6;
            case < 10_000_000: return 7;
            case < 100_000_000: return 8;
            case < 1_000_000_000: return 9;
            default: return 10;
        }
    }
}
