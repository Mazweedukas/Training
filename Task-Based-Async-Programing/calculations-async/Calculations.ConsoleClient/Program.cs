using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Calculations.ConsoleClient
{
    internal static class Program
    {
        /// <summary>
        /// Calculates the sum from 1 to n synchronously.
        /// The value of n is set by the user from the console.
        /// The user can change the boundary n during the calculation, which causes the calculation to be restarted,
        /// this should not crash the application.
        /// After receiving the result, be able to continue calculations without leaving the console.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public static async Task Main()
        {
            var progress = new Progress<(int, long)>((p) => { Console.WriteLine($"Index: {p.Item1}, Sum: {p.Item2}"); });

            while (true)
            {
                Console.WriteLine("Enter n (or Q to quit):");
                var input = Console.ReadLine();

                if (input?.Equals("q", StringComparison.OrdinalIgnoreCase) == true)
                {
                    break;
                }

                if (!int.TryParse(input, out var n))
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                using var cts = new CancellationTokenSource();
                var token = cts.Token;

                var sumTask = Calculator.CalculateSumAsync(n, token, progress);

                Console.WriteLine("Press any key to cancel");

                await MonitorCancellationAsync(sumTask, cts);

                try
                {
                    var result = await sumTask;
                    Console.WriteLine($"Result: {result}");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Cancelled");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Invalid argument: {ex.Message}");
                }
            }
        }

        private static async Task MonitorCancellationAsync(Task task, CancellationTokenSource cts)
        {
            while (!task.IsCompleted)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    await cts.CancelAsync();
                    break;
                }

                await Task.Delay(50);
            }
        }
    }
}
