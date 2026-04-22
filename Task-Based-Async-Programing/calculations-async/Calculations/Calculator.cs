using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Calculations;

/// <summary>
/// Presents methods for the calculation of the sum.
/// </summary>
public static class Calculator
{
    /// <summary>
    /// Calculates the sum from 1 to n synchronously.
    /// </summary>
    /// <param name="n">The last number in the sum.</param>
    /// <returns>A sum: 1 + 2 + ... + n.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throw if n less or equals zero.</exception>
    public static long CalculateSum(int n)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(n, 1);

        int initialNumber = 1;
        long result = 0;

        do
        {
            result += initialNumber;
            initialNumber++;
        }
        while (initialNumber <= n);

        return result;
    }

    /// <summary>
    /// Calculates the sum from 1 to n asynchronously.
    /// </summary>
    /// <param name="n">The last number in the sum.</param>
    /// <param name="token">The cancellation token for the cancellation of the asynchronous operation.</param>
    /// <param name="progress">Presents current status of the asynchronous operation in form of the current value of sum and index.</param>
    /// <returns>A task that represents the asynchronous sum: 1 + 2 + ... + n.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throw if n less or equals zero.</exception>
    public static async Task<long> CalculateSumAsync(int n, CancellationToken token, IProgress<(int, long)>? progress = null)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(n, 1);

        int initialNumber = 1;
        long result = 0;

        long lastReportTime = 0;

        await Task.Run(
        () =>
        {
            var stopwatch = Stopwatch.StartNew();

            do
            {
                result += initialNumber;
                token.ThrowIfCancellationRequested();
                if (stopwatch.ElapsedMilliseconds - lastReportTime >= 500)
                {
                    progress?.Report((initialNumber, result));
                    lastReportTime = stopwatch.ElapsedMilliseconds;
                }

                initialNumber++;
            }
            while (initialNumber <= n);
        }, token);

        return result;
    }
}
