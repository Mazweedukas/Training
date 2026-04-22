using System.Collections.Concurrent;

namespace PalindromeNumberFiltering;

/// <summary>
/// A static class containing methods for filtering palindrome numbers from a collection of integers.
/// </summary>
public static class Selector
{
    /// <summary>
    /// Retrieves a collection of palindrome numbers from the given list of integers using concurrent filtering.
    /// </summary>
    /// <param name="numbers">The list of integers to filter.</param>
    /// <returns>A collection of palindrome numbers.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the input list 'numbers' is null.</exception>
    public static IList<int> GetPalindromes(IList<int> numbers)
    {
        ArgumentNullException.ThrowIfNull(numbers);

        ConcurrentBag<int> palindromeNumbers = new ConcurrentBag<int>();

        using CountdownEvent countdown = new(numbers.Count);

        foreach (int number in numbers)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                if (IsPalindrome(number))
                {
                    palindromeNumbers.Add(number);
                }

                countdown.Signal();
            });
        }

        countdown.Wait();

        return palindromeNumbers.ToList();
    }

    /// <summary>
    /// Checks whether the given integer is a palindrome number.
    /// </summary>
    /// <param name="number">The integer to check.</param>
    /// <returns>True if the number is a palindrome, otherwise false.</returns>
    private static bool IsPalindrome(int number)
    {
        if (number < 0)
        {
            return false;
        }

        return IsPositiveNumberPalindrome(number, 0, GetLength(number) - 1);
    }

    /// <summary>
    /// Recursively checks whether a positive number is a palindrome.
    /// </summary>
    /// <param name="number">The positive number to check.</param>
    /// <param name="left">The index of the leftmost digit to compare.</param>
    /// <param name="right">The index of the rightmost digit to compare.</param>
    /// <returns>True if the positive number is a palindrome, otherwise false.</returns>
    private static bool IsPositiveNumberPalindrome(int number, int left, int right)
    {
        int digitsToCheck = GetLength(number) / 2;

        for (int i = 0; i < digitsToCheck; i++)
        {
            if (GetDigitInDecimalPlace(number, left + i) != GetDigitInDecimalPlace(number, right - i))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Retrieves the digit at a specified decimal place in a given number.
    /// </summary>
    /// <param name="number">The number from which to retrieve the digit.</param>
    /// <param name="decimalPlace">The decimal place (index) of the desired digit, starting from the rightmost digit (ones place).</param>
    /// <returns>The digit at the specified decimal place.</returns>
    private static int GetDigitInDecimalPlace(int number, int decimalPlace)
    {
        int divisor = 1;

        for (int i = 0; i < decimalPlace; i++)
        {
            divisor *= 10;
        }

        return (number / divisor) % 10;
    }

    /// <summary>
    /// Gets the number of digits in the given integer.
    /// </summary>
    /// <param name="number">The integer to count digits for.</param>
    /// <returns>The number of digits in the integer.</returns>
    private static byte GetLength(int number)
    {
        byte length = 0;

        do
        {
            length++;
            number /= 10;
        }
        while (number > 0);

        return length;
    }
}
